using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace UI
{
    public class GiveUpButtonEasterEgg : MonoBehaviour
    {
        private string[] texts = new[]
        {
            "Alas",
            "Are thou certain?",
            "Methinks not",
            "By no means",
            "Must continue mine ascent",
            "Slay more these vile wretches",
            "Forward, ho!",
            "Well wrought",
            "Triumph not belongeth to us"
        };
        private RectTransform _rectTransform;

        private Vector2 _originalPosition;

        private const int MaxHops = 10;
        private int _hopCounter = 0;
        private TextMeshProUGUI _text;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.anchoredPosition;
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void MoveButton()
        {
            if (_hopCounter == MaxHops)
            {
                _rectTransform.anchoredPosition = _originalPosition;
                _text.text = "Thou hast bested me. Fare thee well!";
                return;
            }

            _text.text = texts[Random.Range(0, texts.Length)];

            var newX = Random.Range(-600, 600);
            var newY = Random.Range(-80, -493);
            _rectTransform.anchoredPosition = new Vector3(newX, newY, 0);

            _hopCounter += 1;
        }
    }
}