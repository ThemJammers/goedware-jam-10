using UnityEngine;

namespace Sounds
{
    public static class AudioSourceExtensions
    {
        public static void SetRandomClipFrom(this AudioSource audioSource, AudioClip[] clips)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
        }
    }
}