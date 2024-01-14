using UnityEngine;
using UnityEngine.Events;

namespace halbautomaten.UnityTools.UiTools
{
    [RequireComponent(typeof(Animator))]
    public class UiPanel : MonoBehaviour
    {
        private bool isVisible = false;
        private Animator animator;

        public UnityEvent onShow;
        public UnityEvent onHide;
        
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void Show()
        {
            ToggleVisibility(true);
            onShow.Invoke();
        }

        public virtual void Hide()
        {
            ToggleVisibility(false);
            onHide.Invoke();
        }

        private void ToggleVisibility(bool visible)
        {
            if (visible == isVisible) return;
            isVisible = visible;
            animator.SetBool("show", isVisible);
        }
    }
}
