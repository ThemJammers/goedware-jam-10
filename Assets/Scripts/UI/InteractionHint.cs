using TMPro;
using UnityEngine;

namespace UI
{
    public class InteractionHint : MonoBehaviour
    {
        private Animation fadeInAnimation;
        private TextMeshPro tmp;
        private SpriteRenderer backgroundSpriteRenderer;

        public bool IsVisible { get; private set; }

        public void Start()
        {
            fadeInAnimation = GetComponent<Animation>();
            gameObject.SetActive(false);
        }

        public bool Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
            fadeInAnimation.Play(PlayMode.StopAll);
            return true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }
    }
}