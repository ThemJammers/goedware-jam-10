using Interfaces;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IMovable, IJumpable, ITurnable
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpPower;
        [SerializeField] private float turningSensitivity;
        [SerializeField] private PlayerInput _playerInput;

        private Transform _modelTransform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _modelTransform = transform.GetChild(0);
        }

        public void Move(Vector2 direction)
        {
            if (direction.Equals(Vector2.zero))
            {
                return;
            }
            direction *= IsGrounded() ? movementSpeed : movementSpeed * 0.3f;
            transform.Translate(new Vector3(direction.x, 0, direction.y));
        }
        
        public void Turn(Vector2 input)
        {
            if (_playerInput.IsGamepad)
            {
                TurnWithGamepad(input);
            }
            else
            {
                TurnWithMouse(input);
            }
        }

        private void TurnWithGamepad(Vector2 input)
        {
            //Check if the inputs are above the controller deadzone
            //TODO: change .01f to an actual variable
            if (Mathf.Abs(input.x) > .01f || Mathf.Abs(input.y) > .01f)
            {
                Vector3 playerDirection = Vector3.right * input.x + Vector3.forward * input.y;
                //Only turn if there is an actual turning input
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    _modelTransform.rotation =
                        Quaternion.RotateTowards(_modelTransform.rotation, newRotation, turningSensitivity * Time.fixedTime);
                }
            }
        }

        private void TurnWithMouse(Vector2 mousePosition)
        {
            //Cast a ray from mouse Position to an imaginary ground plane
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
            //Check if the plane was hit
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                //Get the point where the plane was hit and look in that direction
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }

        private void LookAt(Vector3 lookPoint)
        {
            //Ignore the y Position to avoid weird staring upwards of the character
            Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, _modelTransform.position.y, lookPoint.z);
            _modelTransform.LookAt(heightCorrectedPoint);
        }

        public void Jump()
        {
            Jump(jumpPower);
        }
        
        public void Jump(float power)
        {
            //Only jump if player is not already in the air
            if (!IsGrounded()) return;
            _rigidbody.AddForce(Vector3.up * power, ForceMode.Acceleration);
        }

        private bool IsGrounded()
        {
            //Check if a ray shooting down from the character hits the ground in a given distance indicating it is grounded
            int layerMask = 1 << 6;
            return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), (transform.lossyScale.y * 0.5f) + .1f,
                layerMask);
        }
    }
}
