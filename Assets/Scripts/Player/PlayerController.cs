using Core;
using Weapons;
using UnityEngine;

namespace Player
{
    public class PlayerController : GameCharacter
    {
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private Weapon _weapon;
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _weapon = GetComponentInChildren<Weapon>();
        }

        private void Update()
        {
            _playerMovement.Move(_playerInput.MovementVector);
            _playerMovement.Turn(_playerInput.LookDirectionVector);
            if (_playerInput.Jumping) _playerMovement.Jump();
            if (_playerInput.Shooting) _weapon.Shoot();
        }

        public override void Die()
        {
            Debug.Log($"Game Over!");
        }
    }
}
