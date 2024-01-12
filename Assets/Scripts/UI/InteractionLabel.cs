using Patterns;
using UnityEngine;

namespace UI
{
    public class InteractionLabel : Singleton<InteractionLabel>
    {
        [SerializeField] private Animator animator;

        public override void Awake()
        {
            base.Awake();
            if(!animator) animator = GetComponent<Animator>();
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
