using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Mesh[] rockMeshes;
    [SerializeField] MeshFilter meshFilter;
    [Space(10)]
    [SerializeField] private Rigidbody rb;
    [Space(10)]
    [SerializeField][Range(.5f, 5f)] private float rockDeleteSpeed = 1.75f;
    [SerializeField] private ParticleSystem destroyParticles;

    private bool hasWaitedFrame = false;
    private bool isInBucket = false;

    public delegate void OnDeleteRock(Rock rock);
    public OnDeleteRock RockDelete;

    private void Awake()
    {
        meshFilter.mesh = rockMeshes[(int)UnityEngine.Random.value % rockMeshes.Length];
        RockDelete += DeleteRock;
    }

    private void OnDestroy()
    {
        RockDelete -= DeleteRock;
    }

    public void Throw(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.VelocityChange);
    }

    public void DeleteRock()
    {
        RockDelete?.Invoke(this);
    }

    public void OnBucketEnter()
    {
        isInBucket = true;
    }

    private void DeleteRock(Rock rock)
    {
        if (!isInBucket)
        {
            destroyParticles.transform.SetParent(null, false);
            destroyParticles.transform.position = transform.position;
            destroyParticles.Play();
        }

        RockDelete -= DeleteRock;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!hasWaitedFrame)
        {
            hasWaitedFrame = true;
            return;
        }

        if (rb.velocity.magnitude <= rockDeleteSpeed)
            RockDelete?.Invoke(this);
    }
}