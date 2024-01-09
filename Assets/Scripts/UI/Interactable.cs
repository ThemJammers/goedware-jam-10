using System;
using Interfaces;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour, IInteractable
    {
        public InteractionHint Hint { get; private set; }

        private void Start()
        {
            Hint = transform.Find("InteractionHint").TryGetComponent<InteractionHint>(out var result)
                ? result
                : null;
        }

        public void Interact()
        {
            Debug.Log($"Interacting with {name}");
        }
    }
}