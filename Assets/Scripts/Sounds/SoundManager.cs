using UnityEngine;

namespace Sounds
{
    public abstract class SoundManager : MonoBehaviour
    {
        public void PlayClipAt(AudioClip audioClip, Vector3 position, float volume = 1f) =>
            AudioSource.PlayClipAtPoint(audioClip, position, volume);

        public void PlayClipAt(AudioClip[] audioClips, Vector3 position, float volume = 1f)
        {
            var clip = audioClips[Random.Range(0, audioClips.Length)];
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}