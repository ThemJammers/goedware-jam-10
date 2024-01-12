using UnityEngine;
using UnityEngine.UI;

namespace halbautomaten.UnityTools.UiTools
{
    [RequireComponent(typeof(Animator))]

    public class AnimatedToggle : Toggle
    {
        [SerializeField] private Animator animator;

        protected override void Awake()
        {
            base.Awake();
            if (!animator)
            {
                animator = GetComponent<Animator>();
            }
            animator.keepAnimatorStateOnDisable = true;
            onValueChanged.AddListener(OnToggleValueChanged);
            OnToggleValueChanged(this.isOn);
        }

        protected virtual void OnToggleValueChanged(bool toggleState)
        {
            animator.SetBool("isOn", toggleState);
        }
    }
}
