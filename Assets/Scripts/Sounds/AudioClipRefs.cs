using UnityEngine;
using UnityEngine.Serialization;

namespace Sounds
{
    [CreateAssetMenu]
    public class AudioClipRefs : ScriptableObject
    {
        [FormerlySerializedAs("footsteps")] public AudioClip[] footstepsRegular;
        [FormerlySerializedAs("footsteps")] public AudioClip[] footstepsGrass;
        public AudioClip[] repeaterFire;
        public AudioClip[] shotgunFire;
        public AudioClip[] railgunFire;
        public AudioClip railgunDecay;
        public AudioClip[] weaponPickup;
        public AudioClip[] bushCut;
        public AudioClip[] scytheSlice;
        
        public AudioClip[] gruntsBoy;
        public AudioClip[] gruntsSpectralMonster;
    }
}
