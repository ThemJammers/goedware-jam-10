using System;
using Player;
using UnityEngine;
using UnityEngine.VFX;

namespace Sacrifices
{
    public class BloodEffect : MonoBehaviour
    {
        private VisualEffect fx;
        private Transform playerTransform;

        private void Awake()
        {
            fx = GetComponent<VisualEffect>();
            playerTransform = GetComponentInParent<PlayerController>().transform;
        }

        private void Update()
        {
            fx.SetFloat("Collision Height", playerTransform.position.y - 0.2f);
        }
    }

}