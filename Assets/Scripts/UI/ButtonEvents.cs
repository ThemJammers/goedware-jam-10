using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class ButtonEvents : MonoBehaviour
    {
        public UnityEvent onHighlighted;

        public void NotifyHighlighted() => onHighlighted.Invoke();
    }
}