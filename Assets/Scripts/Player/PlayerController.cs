using UnityEngine;

namespace ThemJammers.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            _playerMovement.Move(_playerInput.MovementVector);
            _playerMovement.Turn(_playerInput.LookDirectionVector);
            if (_playerInput.Jumping) _playerMovement.Jump();
        }
    }
}
