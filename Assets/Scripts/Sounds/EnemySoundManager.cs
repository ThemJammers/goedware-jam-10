using Enemies;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemySoundManager : MonoBehaviour
    {
        [SerializeField] public AudioClip[] onDamaged;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void PlayOnDamageTakenSound()
        {
            _audioSource.PlayOneShot(onDamaged);
        }
    }
}