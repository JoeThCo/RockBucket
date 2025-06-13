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

    private bool hasWaitedFrame = false;

    public delegate void OnRockStopped(Rock rock);
    public OnRockStopped RockStopped;

    private void Awake()
    {
        meshFilter.mesh = rockMeshes[(int)UnityEngine.Random.value % rockMeshes.Length];
        RockStopped += rockStopped;
    }

    public void Throw(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.VelocityChange);
    }

    private void rockStopped(Rock rock)
    {
        RockStopped -= rockStopped;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!hasWaitedFrame)
        {
            hasWaitedFrame = true;
            return;
        }

        if (rb.velocity.magnitude <= 5)
        {
            RockStopped?.Invoke(this);
        }
    }
}