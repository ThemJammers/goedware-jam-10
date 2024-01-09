using TMPro;
using UnityEngine;

namespace UI
{
    public class InteractionHint : MonoBehaviour
    {
        public bool IsVisible { get; private set; }

        public void Start()
        {
            gameObject.SetActive(false);
        }

        public bool Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
            return true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }
    }
}