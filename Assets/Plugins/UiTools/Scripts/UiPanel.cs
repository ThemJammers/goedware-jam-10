using UnityEngine;

namespace halbautomaten.UnityTools.UiTools
{
    [RequireComponent(typeof(Animator))]
    public class UiPanel : MonoBehaviour
    {
        private bool isVisible = false;
        private Animator animator;
        
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void Show()
        {
            ToggleVisibility(true);
        }

        public virtual void Hide()
        {
            ToggleVisibility(false);
        }

        private void ToggleVisibility(bool visible)
        {
            if (visible == isVisible) return;
            isVisible = visible;
            animator.SetBool("show", isVisible);
        }
    }
}
