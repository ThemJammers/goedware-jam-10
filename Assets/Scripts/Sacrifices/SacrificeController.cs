using System;
using halbautomaten.UnityTools.UiTools;
using Patterns;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Sacrifices
{
    public class SacrificeController : Singleton<SacrificeController>
    {
        public float PlayerSpeedModifier
        {
            get;
            set;
        } = 1;

        public float PlayerProjectileModifier
        {
            get;
            set;
        } = 1;

        public float PlayerCadenceModifier
        {
            get;
            set;
        } = 1;

        public float PlayerMeleeModifier
        {
            get;
            set;
        } = 1;        
        public float EnemySpeedModifier {
            get;
            set;
        }

        public float EnemyProjectileModifier
        {
            get;
            set;
        } = 1;

        public float EnemyMeleeModifier
        {
            get;
            set;
        } = 1;

        [SerializeField] private TextMeshProUGUI positiveEffectTooltip;
        [SerializeField] private TextMeshProUGUI negativeEffectTooltip;

        public UnityEvent EyeSacrificed;
        public UnityEvent ArmSacrificed;
        public UnityEvent LegSacrificed;
        
        private SacrificeEffect _activeEffect;

        private void Start()
        {
            UpdateTooltips();
        }

        public void SetSacrificeEffect(SacrificeEffect effect)
        {
            Debug.Log(effect.name);
            _activeEffect = effect;
            UpdateTooltips();
        }

        private void UpdateTooltips()
        {
            string positiveTooltip = _activeEffect != null ? $"{_activeEffect.positiveTooltipKey}: x{_activeEffect.positiveModifier}" : "HardMode, player receives more damage"; 
            string negativeTooltip = _activeEffect != null ? $"{_activeEffect.negativeTooltipKey}: x{_activeEffect.negativeModifier}" : "HardMode, enemies receive less damage"; 
            positiveEffectTooltip.text = positiveTooltip;
            negativeEffectTooltip.text = negativeTooltip;
        }

        public void ApplySacrificeEffect()
        {
            if(_activeEffect != null)
                _activeEffect.Apply();
            GetComponentInChildren<UiPanel>().Hide();
        }

        public void SacrificeBodyPart(BodyPartSacrifice bodyPartSacrifice)
        {
            switch (bodyPartSacrifice)
            {
                case BodyPartSacrifice.Eye:
                    EyeSacrificed?.Invoke();
                    break;
                case BodyPartSacrifice.Arm:
                    ArmSacrificed?.Invoke();
                    break;
                case BodyPartSacrifice.Leg:
                    LegSacrificed?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bodyPartSacrifice), bodyPartSacrifice, null);
            }
        }
    }
}
