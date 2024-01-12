using System;
using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;

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

        // Components
        private MeshFilter meshFilter;
        private MeshCollider meshCollider;

        // Internals
        private int stepIdx = 0;
        [CanBeNull] private Coroutine growCoroutine = null;


        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();

            var growVicinityCollider = GetComponentInChildren<GrowVicinityCollider>();
            growVicinityCollider.PlayerVicinityRadius = playerVicinityRadius;
            growVicinityCollider.onPlayerEnter.AddListener(_ => StopGrowing());
            growVicinityCollider.onPlayerExit.AddListener(StartGrowing);
        }

        private void SetMesh(Mesh mesh)
        {
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
        }

        private void StartGrowing()
        {
            if (growCoroutine != null) StopCoroutine(growCoroutine);
            growCoroutine = StartCoroutine(nameof(Grow));
            Debug.Log("Start");
        }

        private void StopGrowing()
        {
            if (growCoroutine != null) StopCoroutine(growCoroutine);
            growCoroutine = null;
            Debug.Log("Stop");
        }

        private void Start()
        {
            growCoroutine = StartCoroutine(nameof(Grow));
        }

        private IEnumerator Grow()
        {
            while (stepIdx < meshSteps.Length)
            {
                yield return new WaitForSeconds(growStepSeconds);

                SetMesh(meshSteps[stepIdx]);
                stepIdx += 1;
            }
        }

    }
}