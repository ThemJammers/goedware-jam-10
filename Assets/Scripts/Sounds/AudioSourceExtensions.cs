using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public static class AudioSourceExtensions
    {
        public static void SetRandomClipFrom(this AudioSource audioSource, IList<AudioClip> clips) =>
            audioSource.clip = clips[Random.Range(0, clips.Count)];

        public static void PlayOneShot(this AudioSource audioSource, IList<AudioClip> clips) =>
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }
}