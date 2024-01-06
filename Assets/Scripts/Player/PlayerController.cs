using ThemJammers.Core;

namespace ThemJammers.Player
{
    public class PlayerController : GameCharacter
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
