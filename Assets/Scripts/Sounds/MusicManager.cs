using System.Linq;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private MusicRefs musicRefs;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!audioSource.isPlaying)
            {
                ChooseAndPlayRandomTrack();
            }
        }

        private void Start()
        {
            audioSource.clip = musicRefs.normal[0];
            audioSource.Play();
        }

        private void ChooseAndPlayRandomTrack()
        {
            // TODO: Until we have battle/normal state triggers, just play them randomly.
            var combinedClips = musicRefs.normal.Concat(musicRefs.battle);
            audioSource.SetRandomClipFrom(combinedClips.ToArray());
            audioSource.Play();
        }
    }
}