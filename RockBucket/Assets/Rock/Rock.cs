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
    private float deleteTime = 0;
    [SerializeField][Range(1f, 5f)] private float RockOnGroundDeleteTime = 2.5f;
    [SerializeField][Range(.5f, 5f)] private float rockDeleteSpeed = 1.75f;
    [Space(10)]
    [SerializeField] private ParticleSystem destroyParticles;

    private bool isInBucket = false;

    public delegate void RockEvent(Rock rock);
    public event RockEvent RockDelete;

    private void Awake()
    {
        meshFilter.mesh = rockMeshes[(int)UnityEngine.Random.value % rockMeshes.Length];
        transform.rotation = UnityEngine.Random.rotation;
        RockDelete += Rock_RockDelete;
    }

    private void OnDestroy()
    {
        RockDelete += Rock_RockDelete;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RockDelete?.Invoke(this);
        }
    }

    public void Throw(Vector3 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode.VelocityChange);
    }

    public void DeleteRock()
    {
        RockDelete?.Invoke(this);
    }

    public void OnBucketEnter()
    {
        isInBucket = true;
    }

    private void Rock_RockDelete(Rock rock)
    {
        if (!isInBucket)
        {
            destroyParticles.transform.SetParent(null, false);
            destroyParticles.transform.position = transform.position;
            destroyParticles.Play();
        }

        RockDelete -= Rock_RockDelete;
        Destroy(gameObject);
    }

    public void ApplyWind(Vector3 direction, float power)
    {
        rb.AddForce(direction.normalized * power, ForceMode.Acceleration);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && rb.velocity.magnitude <= rockDeleteSpeed)
        {
            deleteTime += Time.deltaTime;
            if (deleteTime >= RockOnGroundDeleteTime)
                RockDelete?.Invoke(this);
        }
    }
}