using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBucket : MonoBehaviour
{
    [SerializeField] private ParticleSystem celebrateParticles;

    public delegate void BucketEvent(Rock rock);
    public event BucketEvent RockInBucket;

    private void Start()
    {
        RockInBucket += InBucket_RockInBucket;
    }

    private void OnDisable()
    {
        RockInBucket -= InBucket_RockInBucket;
    }

    private void InBucket_RockInBucket(Rock rock)
    {
        SoundEffectController.Play("InBucket", transform.position);
        celebrateParticles.Play();
        rock.OnBucketEnter();
        rock.DeleteRock();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rock rock = other.gameObject.GetComponent<Rock>();
        if (rock == null) return;
        RockInBucket?.Invoke(rock);
    }
}