using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBucket : MonoBehaviour
{
    [SerializeField] private ParticleSystem celebrateParticles;

    public delegate void BucketEvent(Rock rock);
    public BucketEvent RockInBucket;

    private void Start()
    {
        RockInBucket += rockInBucket;
    }

    private void OnDisable()
    {
        RockInBucket -= rockInBucket;
    }

    private void rockInBucket(Rock rock)
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