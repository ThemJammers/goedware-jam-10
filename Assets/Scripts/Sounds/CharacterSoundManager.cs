using System;
using Data;
using Player;
using UnityEngine;
using UnityEngine.Pool;
using Weapons;
using Random = UnityEngine.Random;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource), typeof(AudioSource))]
    public class CharacterSoundManager : SoundManager
    {
        [SerializeField] private AudioClipRefs audioClipRefs;

        private AudioSource footstepsAudioSource;
        private AudioSource weaponFireAudioSource;

        private PlayerController playerController;
        private PlayerWeaponController playerWeaponController;

        private void Start()
        {
            var parent = transform.parent;
            playerController = parent.GetComponent<PlayerController>();
            playerWeaponController = parent.GetComponent<PlayerWeaponController>();
            var audioSourceComponents = GetComponents<AudioSource>();
            footstepsAudioSource = audioSourceComponents[0];
            weaponFireAudioSource = audioSourceComponents[1];
            weaponFireAudioSource.loop = false;

            // TODO: Implement grass walking sounds (footsteps[1])
            footstepsAudioSource.clip = audioClipRefs.footsteps[0];

            playerController.onCharacterMoving.AddListener(() => footstepsAudioSource.Play());
            playerController.onCharacterIdle.AddListener(() => footstepsAudioSource.Stop());

            playerWeaponController.onProjectileAdded.AddListener(() =>
                PlayClipAt(audioClipRefs.weaponPickup, transform.position));
            playerWeaponController.Weapon.onProjectileChanged.AddListener(OnWeaponProjectileChanged);
            playerWeaponController.Weapon.onShoot.AddListener(() => weaponFireAudioSource.Play());
        }


        private void OnWeaponProjectileChanged(ProjectileData projectileData)
        {
            if (projectileData.name.Contains("Repeater"))
                weaponFireAudioSource.clip = audioClipRefs.repeaterFire[0];
            else if (projectileData.name.Contains("Shotgun"))
                weaponFireAudioSource.clip = audioClipRefs.shotgunFire[0];
            else if (projectileData.name.Contains("Railgun"))
                weaponFireAudioSource.clip = audioClipRefs.railgunFire[0];
        }
    }
}