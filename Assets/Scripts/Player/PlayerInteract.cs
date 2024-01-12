using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerInteract : MonoBehaviour
    {
        private ISet<(Collider, IInteractable)>
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
        }

        public void Interact()
        {
            if (closestInteractable == null) return;
            closestInteractable.Interact();
        }

        private void UpdateClosestInteractable()
        {
            if (interactablesInRange.Count == 0)
            {
                return;
            }

            var sorted = interactablesInRange.Where(x => x.Item1 != null && x.Item2 != null).ToList();
            sorted.Sort(new DistanceComparer(transform));
            interactablesInRange = sorted.ToHashSet();

            if (interactablesInRange.Count == 0)
            {
                return;
            }

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