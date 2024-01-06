using System;
using ThemJammers.Interfaces;
using UnityEngine;
using UnityEngine.Animations;

namespace ThemJammers.Player
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
            direction *= movementSpeed;
            _rigidbody.AddForce(new Vector3(direction.x, 0, direction.y), ForceMode.Impulse);
        }
        
        public void Turn(Vector2 direction)
        {
            if (_playerInput.IsGamepad)
            {
                TurnWithGamepad(direction);
            }
            else
            {
                TurnWithMouse(direction);
            }
        }

        private void TurnWithGamepad(Vector2 direction)
        {
            //Check if the inputs are above the controller deadzone
            //TODO: change .01f to an actual variable
            if (Mathf.Abs(direction.x) > .01f || Mathf.Abs(direction.y) > .01f)
            {
                Vector3 playerDirection = Vector3.right * direction.x + Vector3.forward * direction.y;
                //Only turn if there is an actual turning input
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    _modelTransform.rotation =
                        Quaternion.RotateTowards(_modelTransform.rotation, newRotation, turningSensitivity * Time.fixedTime);
                }
            }
        }

        private void TurnWithMouse(Vector2 direction)
        {
            //Cast a ray from mouse Position to an imaginary ground plane
            Ray ray = Camera.main.ScreenPointToRay(direction);
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
            _rigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
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