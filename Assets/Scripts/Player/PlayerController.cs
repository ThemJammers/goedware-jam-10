using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Sacrifices;
using Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : GameCharacter
    {
        private enum MovementState
        {
            Moving,
            Idle
        }

        public UnityEvent onPlayerDied;

        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private Weapon _weapon;
        private PlayerWeaponController _playerWeaponController;
        private PlayerAnimations _playerAnimations;
        private PlayerInteract _playerInteract;
        private Coroutine meleeRoutine = null;

        public UnityEvent onCharacterMoving;
        public UnityEvent onCharacterIdle;

        private MovementState _movementState = MovementState.Idle;
        public ISet<Collider> ObjectsHitInLastMeleeAttack { get; private set; } = new HashSet<Collider>();

        private void Awake()
        {
            //TODO: Shift this into gameManager
            Application.targetFrameRate = 60;

            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _weapon = GetComponentInChildren<Weapon>();
            _playerWeaponController = GetComponentInChildren<PlayerWeaponController>();
            _playerAnimations = GetComponent<PlayerAnimations>();
            _playerInteract = GetComponentInChildren<PlayerInteract>();
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
            if (_playerInput.Interact) _playerInteract.Interact();
            _playerWeaponController.SelectWeapon(_playerInput.WeaponSelection);
            if (_playerInput.Melee)
            {
                meleeRoutine ??= StartCoroutine(TriggerMeleeAttack());
                return;
            }

            if (_weapon.isActiveAndEnabled)
            {
                if (_playerInput.Shooting) _weapon.Shoot();
            }
        }

        private void HandleAnimations()
        {
            if (_playerInput.MovementVector != Vector2.zero)
            {
                int movementDirection = _playerMovement.WalkLookAngle > 90 ? -1 : 1;
                _playerAnimations.PlayMove(movementDirection);

                if (_movementState != MovementState.Moving) onCharacterMoving?.Invoke();
                _movementState = MovementState.Moving;
            }
            else
            {
                _playerAnimations.PlayIdle();
                if (_movementState != MovementState.Idle) onCharacterIdle?.Invoke();
                _movementState = MovementState.Idle;
            }
        }

        public override void Die()
        {
            onPlayerDied?.Invoke();
        }

        private IEnumerator TriggerMeleeAttack()
        {
            _weapon.gameObject.SetActive(false);
            _playerAnimations.PlayMelee();
            yield return new WaitForSecondsRealtime(.5f);
            ObjectsHitInLastMeleeAttack = new HashSet<Collider>();
            _weapon.gameObject.SetActive(true);
            meleeRoutine = null;
        }
    }
}