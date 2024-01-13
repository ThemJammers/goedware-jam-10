using UnityEngine;

namespace Sounds
{
    public class FootstepsEventReceiver : MonoBehaviour
    {
        private PlayerSoundManager _soundManager;

        private void Awake()
        {
            _soundManager = transform.parent.Find("CharacterSoundManager").GetComponent<PlayerSoundManager>();
        }

        public void FootstepLeft()
        {
            _soundManager.OnFootstep();
        }

        public void FootstepRight()
        {
            _soundManager.OnFootstep();
        }
    }
}