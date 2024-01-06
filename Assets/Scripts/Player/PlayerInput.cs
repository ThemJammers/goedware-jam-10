using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThemJammers.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 MovementVector { get; private set; }
        public Vector2 LookDirectionVector { get; private set; }
        public bool Jumping { get; private set;}

        public bool IsGamepad = false;
        private CustomInput _input;

        private void Awake()
        {
            _input = new CustomInput();
            _input.Enable();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }

        private void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
            MovementVector = _input.Player.Movement.ReadValue<Vector2>();
            LookDirectionVector = _input.Player.LookDirection.ReadValue<Vector2>();
            Jumping = _input.Player.Jumping.ReadValue<float>() != 0;
        }

        public void OnDeviceChanged(UnityEngine.InputSystem.PlayerInput input)
        {
            IsGamepad = input.currentControlScheme.Equals("Gamepad");
        }
    }
}
