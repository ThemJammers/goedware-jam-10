using Patterns;
using UnityEngine;

namespace UI
{
    public class InteractionLabel : Singleton<InteractionLabel>
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Show()
        {
            animator.SetBool("show", true);
        }

        public void Hide()
        {
            animator.SetBool("show", false);
        }
    }
}
