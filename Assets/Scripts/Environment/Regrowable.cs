using System;
using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Regrowable : MonoBehaviour
    {
        // Fields
        [SerializeField] private int growStepSeconds = 60;
        [SerializeField] private Mesh[] meshSteps;
        [SerializeField] private float playerVicinityRadius = 20f;

        // Events
        public UnityEvent<int, int> onCutDown;

        // Components
        private MeshFilter meshFilter;
        private MeshCollider meshCollider;
        [CanBeNull] private TouchDamageOverTime touchDamageOverTime;

        // Internals
        private int stepIdx = 0;
        [CanBeNull] private Coroutine growCoroutine = null;


        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();
            touchDamageOverTime = GetComponentInChildren<TouchDamageOverTime>();

            // Initialize
            SetMeshIdx(0);

            var growVicinityCollider = GetComponentInChildren<GrowVicinityCollider>();
            growVicinityCollider.PlayerVicinityRadius = playerVicinityRadius;
            growVicinityCollider.onPlayerEnter.AddListener(_ => StopGrowing());
            growVicinityCollider.onPlayerExit.AddListener(StartGrowing);
        }

        private void SetMeshIdx(int idx)
        {
            var mesh = meshSteps[0];
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            if (touchDamageOverTime != null && idx == 0)
            {
                // A little hack that's needed because the OnTriggerExit of the sub game object won't be called when the mesh is changed to null
                touchDamageOverTime.Reset();
            }
        }

        private void StartGrowing()
        {
            if (growCoroutine != null) StopCoroutine(growCoroutine);
            growCoroutine = StartCoroutine(nameof(Grow));
        }

        private void StopGrowing()
        {
            if (growCoroutine != null) StopCoroutine(growCoroutine);
            growCoroutine = null;
        }

        private void Start()
        {
            growCoroutine = StartCoroutine(nameof(Grow));
        }

        private IEnumerator Grow()
        {
            while (stepIdx < meshSteps.Length - 1)
            {
                yield return new WaitForSeconds(growStepSeconds);

                stepIdx += 1;
                SetMeshIdx(stepIdx);
            }
        }

        public void CutDown()
        {
            if (stepIdx <= 0) return; // Shouldn't happen as there is no mesh to collide with

            stepIdx -= 1;
            Debug.Log($"Cut down to {stepIdx}");
            SetMeshIdx(stepIdx);

            onCutDown.Invoke(stepIdx, meshSteps.Length);
        }
    }
}