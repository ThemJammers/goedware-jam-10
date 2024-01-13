using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private MusicRefs musicRefs;

        public UnityEvent<AudioClip> onCurrentlyPlaying;

        private AudioSource _audioSource;

        private AudioClip _currentlyPlaying;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_audioSource.time >= _audioSource.clip.length)
            {
                // Finished last track. Start next
                ChooseAndPlayRandomTrack();
            }
        }

        private void Start()
        {
            _audioSource.clip = musicRefs.normal[0];
            _audioSource.Play();
            onCurrentlyPlaying.Invoke(_audioSource.clip);
        }

        private void ChooseAndPlayRandomTrack()
        {
            // TODO: Until we have battle/normal state triggers, just play them randomly.
            var combinedClips = musicRefs.normal.Concat(musicRefs.battle);
            _audioSource.SetRandomClipFrom(combinedClips.ToArray());
            _audioSource.Play();
            onCurrentlyPlaying.Invoke(_audioSource.clip);
        }
    }
}