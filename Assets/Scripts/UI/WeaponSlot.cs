using Data;
using JetBrains.Annotations;
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

        [CanBeNull] private ProjectileData projectile;

        private static readonly int Active = Animator.StringToHash("active");
        private static readonly int Collected = Animator.StringToHash("collected");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _toggle = GetComponent<Toggle>();
            _label = GetComponentInChildren<TextMeshProUGUI>();

            _toggle.onValueChanged.AddListener((newValue) => { _animator.SetBool(Active, newValue); });
            _label.text = "";
        }

        public void Activate()
        {
            _toggle.SetIsOnWithoutNotify(true);
        }

        public void SetCollected(ProjectileData projectileData)
        {
            projectile = projectileData;
            _animator.SetBool(Collected, true);

            // TODO: user-visible names
            _label.text = projectileData.name.Replace("Projectile", "").Trim();
        }
    }
}