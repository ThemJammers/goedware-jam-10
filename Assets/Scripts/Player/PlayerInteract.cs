using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerInteract : MonoBehaviour
    {
        private readonly ISet<(Collider, IInteractable)>
            interactablesInRange = new HashSet<(Collider, IInteractable)>();

        private IInteractable closestInteractable;

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            if (!other.transform.TryGetComponent(out IInteractable interactable)) return;
            // ReSharper disable once Unity.NoNullPropagation
            interactable.Hint?.Show();
            interactablesInRange.Add((other, interactable));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == null) return;
            if (!other.transform.TryGetComponent(out IInteractable interactable)) return;
            // ReSharper disable once Unity.NoNullPropagation
            interactable.Hint?.Hide();
            interactablesInRange.Remove((other, interactable));
        }

        private void Update()
        {
            UpdateClosestInteractable();

            if (closestInteractable == null) return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                closestInteractable.Interact();
            }
        }

        private void UpdateClosestInteractable()
        {
            if (interactablesInRange.Count == 0)
            {
                return;
            }

            var sorted = interactablesInRange.ToList();
            sorted.Sort(new DistanceComparer(transform));
            closestInteractable = sorted.First().Item2;
        }

        private class DistanceComparer : IComparer<(Collider, IInteractable)>
        {
            private readonly Transform transform;

            public DistanceComparer(Transform transform)
            {
                this.transform = transform;
            }

            public int Compare((Collider, IInteractable) a, (Collider, IInteractable) b)
            {
                if (a.Item1 == null || b.Item1 == null)
                    throw new InvalidOperationException("Can't work with null colliders");

                var ownPosition = transform.position;
                var distA = Vector3.Distance(ownPosition, a.Item1.transform.position);
                var distB = Vector3.Distance(ownPosition, b.Item1.transform.position);

                var diff = distA - distB;
                return diff == 0 ? 0 : diff > 0 ? 1 : -1;
            }
        }
    }
}