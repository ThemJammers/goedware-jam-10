using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu]
    public class MusicRefs : ScriptableObject
    {
        public AudioClip[] normal;
        public AudioClip[] battle;
        public AudioClip[] menu;
    }
}