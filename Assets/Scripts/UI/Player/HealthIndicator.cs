using System;
using Core;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Player
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private GameCharacter _gameCharacter;

        private void Awake()
        {
            _gameCharacter = GetComponentInParent<GameCharacter>();
            _gameCharacter.HealthChanged.AddListener(onHealthChanged);
        }

        private void Start()
        {
            //Update health initially
            //Probably 100
            onHealthChanged();
        }
        
        private void OnDestroy()
        {
            _gameCharacter.HealthChanged.RemoveAllListeners();
        }

        private void onHealthChanged()
        {
            //Update horizontal image fill of health bar as a value between 0-1 based on the characters health
            _image.fillAmount = ((float)_gameCharacter.Health / 100f);
        }
    }
}