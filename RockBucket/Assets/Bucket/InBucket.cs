using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBucket : MonoBehaviour
{
    [SerializeField] private ParticleSystem celebrateParticles;

    public delegate void BucketEvent(Rock rock);
    public event BucketEvent RockInBucket;

    public static bool IsRockInBucket = false;

    private void Start()
    {
        RockInBucket += InBucket_RockInBucket;
    }

    private void OnDisable()
    {
        RockInBucket -= InBucket_RockInBucket;
    }

    private static void OnRockInBucket() 
    {
        IsRockInBucket = true;
    }

    public static void OnRockInBucketReset() 
    {
        IsRockInBucket = false;
    }

    private void InBucket_RockInBucket(Rock rock)
    {
        OnRockInBucket();

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