using System;
using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Environment
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Collider))]
    public class Regrowable : MonoBehaviour
    {
        // Fields
        [FormerlySerializedAs("growStepSeconds")] [SerializeField]
        private int averageGrowStepSeconds = 60;

        [SerializeField] private Mesh[] meshSteps;
        [SerializeField] private float playerVicinityRadius = 20f;

        // Events
        public UnityEvent<int, int> onCutDown;

        // Components
        private MeshFilter _meshFilter;
        [CanBeNull] private MeshCollider _meshCollider;
        [CanBeNull] private TouchDamageOverTime _touchDamageOverTime;

        // Internals
        private int _stepIdx = 0;
        [CanBeNull] private Coroutine _growCoroutine = null;


        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();

            TryGetComponent(out _meshCollider);
            _touchDamageOverTime = GetComponentInChildren<TouchDamageOverTime>();

            // Initialize
            SetMeshIdx(0);

            var growVicinityCollider = GetComponentInChildren<GrowVicinityCollider>();
            growVicinityCollider.PlayerVicinityRadius = playerVicinityRadius;
            growVicinityCollider.onPlayerEnter.AddListener(_ => StopGrowing());
            growVicinityCollider.onPlayerExit.AddListener(StartGrowing);
        }

        private void SetMeshIdx(int idx)
        {
            if (idx == 0)
            {
                _meshFilter.mesh = null;
                if (_meshCollider != null) _meshCollider.sharedMesh = null;

                if (_touchDamageOverTime != null)
                {
                    // A little hack that's needed because the OnTriggerExit of the sub game object won't be called when the mesh is changed to null
                    _touchDamageOverTime.Reset();
                }
            }
            else
            {
                var mesh = meshSteps[idx - 1];
                _meshFilter.mesh = mesh;
                if (_meshCollider != null) _meshCollider.sharedMesh = mesh;
            }
        }

        private void StartGrowing()
        {
            if (_growCoroutine != null) StopCoroutine(_growCoroutine);
            _growCoroutine = StartCoroutine(nameof(Grow));
        }

        private void StopGrowing()
        {
            if (_growCoroutine != null) StopCoroutine(_growCoroutine);
            _growCoroutine = null;
        }

        private void Start()
        {
            _growCoroutine = StartCoroutine(nameof(Grow));
        }

        private IEnumerator Grow()
        {
            while (_stepIdx < meshSteps.Length)
            {
                var waitFor = Random.Range(averageGrowStepSeconds / 2,
                    averageGrowStepSeconds + (averageGrowStepSeconds / 2));
                yield return new WaitForSeconds(waitFor);

                _stepIdx += 1;
                SetMeshIdx(_stepIdx);
            }
        }

        public void CutDown()
        {
            if (_stepIdx <= 0) return; // Shouldn't happen as there is no mesh to collide with

            _stepIdx -= 1;
            SetMeshIdx(_stepIdx);

            onCutDown.Invoke(_stepIdx, meshSteps.Length);
        }
    }
}