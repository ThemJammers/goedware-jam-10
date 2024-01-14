using Data;
using Player;
using UnityEngine;
using Weapons;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource), typeof(AudioSource), typeof(AudioSource))]
    public class PlayerSoundManager : SoundManager
    {
        [SerializeField] private AudioClipRefs audioClipRefs;

        private AudioSource _footstepsAudioSource;
        private AudioSource _weaponFireAudioSource;
        private AudioSource _oneshotAudioSource;

        private PlayerController _playerController;
        private PlayerWeaponController _playerWeaponController;

        private void Start()
        {
            var parent = transform.parent;
            _playerController = parent.GetComponent<PlayerController>();
            _playerWeaponController = parent.GetComponent<PlayerWeaponController>();
            var audioSourceComponents = GetComponents<AudioSource>();
            _footstepsAudioSource = audioSourceComponents[0];
            _weaponFireAudioSource = audioSourceComponents[1];
            _oneshotAudioSource = audioSourceComponents[2];
            _weaponFireAudioSource.loop = false;


            _playerController.onMeleeAttack.AddListener(
                () => _oneshotAudioSource.PlayOneShot(audioClipRefs.scytheSlice));

            _playerController.onBushCut.AddListener(() => _oneshotAudioSource.PlayOneShot(audioClipRefs.bushCut));
            _playerController.onDamageTaken.AddListener(() => _oneshotAudioSource.PlayOneShot(audioClipRefs.gruntsBoy));

            _playerWeaponController.onProjectileAdded.AddListener(() =>
                _oneshotAudioSource.PlayOneShot(audioClipRefs.weaponPickup[0]));
            _playerWeaponController.Weapon.onProjectileChanged.AddListener(OnWeaponProjectileChanged);
            _playerWeaponController.Weapon.onShoot.AddListener(() =>
                _weaponFireAudioSource.PlayOneShot(_weaponFireAudioSource.clip));
            _playerWeaponController.Weapon.onShoot.AddListener(() =>
                _weaponFireAudioSource.PlayOneShot(_weaponFireAudioSource.clip));

            _playerController.onCharacterStopShooting.AddListener(() =>
            {
                if (_weaponFireAudioSource.clip == audioClipRefs.railgunFire[0])
                {
                    _oneshotAudioSource.PlayOneShot(audioClipRefs.railgunDecay);
                }
            });
        }


        private void OnWeaponProjectileChanged(ProjectileData projectileData)
        {
            if (projectileData.name.Contains("Repeater"))
                _weaponFireAudioSource.clip = audioClipRefs.repeaterFire[0];
            else if (projectileData.name.Contains("Shotgun"))
                _weaponFireAudioSource.clip = audioClipRefs.shotgunFire[0];
            else if (projectileData.name.Contains("Railgun"))
                _weaponFireAudioSource.clip = audioClipRefs.railgunFire[0];
        }

        public void OnFootstep()
        {
            _footstepsAudioSource.PlayOneShot(audioClipRefs.footstepsRegular);
        }
    }
}