using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponSlot : MonoBehaviour
    {
        private Animator _animator;
        private Toggle _toggle;
        private TextMeshProUGUI _label;
        private Image _backgroundImage;

        private static readonly int Active = Animator.StringToHash("active");
        private static readonly int Collected = Animator.StringToHash("collected");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _toggle = GetComponent<Toggle>();
            
            // TODO: Better get these as serialized assets. Find sucks
            _label = transform.Find("Label").GetComponent<TextMeshProUGUI>();
            _backgroundImage = transform.Find("ProjectileSprite").GetComponent<Image>();

            _toggle.onValueChanged.AddListener((newValue) => { _animator.SetBool(Active, newValue); });
            _label.text = "";
        }

        public void Activate()
        {
            _toggle.SetIsOnWithoutNotify(true);
        }

        public void SetCollected(ProjectileData projectileData)
        {
            _animator.SetBool(Collected, true);

            // TODO: user-visible names
            _label.text = projectileData.name.Replace("Projectile", "").Trim();

            _backgroundImage.sprite = projectileData.sprite;
        }
    }
}