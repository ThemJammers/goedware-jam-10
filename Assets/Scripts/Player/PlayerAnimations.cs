using UnityEngine;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void PlayIdle()
        {
            animator.SetBool("Backwards", false);
            animator.SetBool("IsMoving", false);
        }

        public void PlayMove(int direction)
        {
            animator.SetBool("Backwards", direction < 0);
            animator.SetBool("IsMoving", true);
        }

        public void PlayMelee()
        {
            animator.SetBool("Melee", true);
        }
    }
}
