using System.Collections;
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
        private MeleeWeapon _meleeWeapon;
        private PlayerWeaponController _playerWeaponController;
        private PlayerAnimations _playerAnimations;
        private Coroutine meleeRoutine = null;
        private void Awake()
        {
            //TODO: Shift this into gameManager
            Application.targetFrameRate = 60;
            
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _weapon = GetComponentInChildren<Weapon>();
            _meleeWeapon = GetComponentInChildren<MeleeWeapon>(true);
            _playerWeaponController = GetComponentInChildren<PlayerWeaponController>();
            _playerAnimations = GetComponent<PlayerAnimations>();
        }

        private void Update()
        {
            HandleInputs();
            HandleAnimations();
        }

        private void HandleInputs()
        {
            _playerMovement.Move(_playerInput.MovementVector);
            _playerMovement.Turn(_playerInput.LookDirectionVector);
            if (_playerInput.Jumping) _playerMovement.Jump();
            if (_weapon.isActiveAndEnabled)
            {
                if (_playerInput.Shooting) _weapon.Shoot();
            }
            _playerWeaponController.SelectWeapon(_playerInput.WeaponSelection);
            if (_playerInput.Melee)
            {
                if (meleeRoutine == null)
                {
                    meleeRoutine = StartCoroutine(TriggerMeleeAttack());
                }
            }
        }

        private void HandleAnimations()
        {
            if (_playerInput.MovementVector != Vector2.zero)
            {
                int movementDirection = _playerMovement.WalkLookAngle > 90 ? -1 : 1;
                _playerAnimations.PlayMove(movementDirection);
            }
            else
            {
                _playerAnimations.PlayIdle();
            }
        }

        public override void Die()
        {
            Debug.Log($"Game Over!");
        }

        private IEnumerator TriggerMeleeAttack()
        {
            _weapon.gameObject.SetActive(false);
            _meleeWeapon.gameObject.SetActive(true);
            _playerAnimations.PlayMelee();
            yield return new WaitForSecondsRealtime(.5f);
            _meleeWeapon.gameObject.SetActive(false);
            _weapon.gameObject.SetActive(true);
            meleeRoutine = null;
        }
    }
}
