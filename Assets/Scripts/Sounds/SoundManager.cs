using UnityEngine;

namespace Sounds
{
    public abstract class SoundManager : MonoBehaviour
    {
        protected static void PlayClipAt(AudioClip audioClip, Vector3 position, float volume = 1.0f) =>
            AudioSource.PlayClipAtPoint(audioClip, position, volume);

        protected static void PlayClipAt(AudioClip[] audioClips, Vector3 position, float volume = 1.0f)
        {
            var clip = audioClips[Random.Range(0, audioClips.Length)];
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}