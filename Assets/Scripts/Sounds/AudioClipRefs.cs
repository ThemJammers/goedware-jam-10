using UnityEngine;
using UnityEngine.Serialization;

namespace Sounds
{
    [CreateAssetMenu()]
    public class AudioClipRefs : ScriptableObject
    {
        [FormerlySerializedAs("footstep")] public AudioClip[] footsteps;
        public AudioClip[] repeaterFire;
        public AudioClip[] shotgunFire;
        public AudioClip[] railgunFire;
        public AudioClip[] weaponPickup;
    }
}
