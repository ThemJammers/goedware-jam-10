using Data;
using Player;
using UnityEngine;
using Weapons;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class EnemySoundManager : SoundManager
    {
        [SerializeField] protected AudioClipRefs audioClipRefs;

        private AudioSource _audioSource;

        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public abstract AudioClip[] DamageTakenSound { get; }

        public void PlayOnDamageTakenSound()
        {
            _audioSource.PlayOneShot(DamageTakenSound);
        }
    }
}