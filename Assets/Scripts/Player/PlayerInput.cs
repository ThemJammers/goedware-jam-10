using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 MovementVector { get; private set; }
        public Vector2 LookDirectionVector { get; private set; }
        public bool Jumping { get; private set; }
        public bool Shooting { get; private set; }
        public bool Melee { get; private set; }

        public bool IsGamepad { get; private set; }
        public int WeaponSelection { get; private set; }
        private CustomInput _input;

        private void Awake()
        {
            _input = new CustomInput();
            _input.Enable();
        }

        private void OnDestroy()
        {
            _input?.Disable();
        }

        private void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
            if (_input == null) return;

            MovementVector = _input.Player.Movement.ReadValue<Vector2>();
            LookDirectionVector = _input.Player.LookDirection.ReadValue<Vector2>();
            Jumping = _input.Player.Jumping.ReadValue<float>() != 0;
            Melee = _input.Player.Melee.ReadValue<float>() != 0;
            Shooting = _input.Player.Shoot.ReadValue<float>() != 0;
            if (_input.Player.WeaponSelection.triggered)
            {
                WeaponSelection = (int)_input.Player.WeaponSelection.ReadValue<float>();
            }
            Melee = _input.Player.Melee.triggered;
        }

        public void OnDeviceChanged(UnityEngine.InputSystem.PlayerInput input)
        {
            IsGamepad = input.currentControlScheme.Equals("Gamepad");
        }
    }
}