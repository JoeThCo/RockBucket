using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBucket : MonoBehaviour
{
    [SerializeField] private ParticleSystem celebrateParticles;
    private void OnTriggerEnter(Collider other)
    {
        Rock rock = other.gameObject.GetComponent<Rock>();
        if (rock != null) 
        {
            celebrateParticles.Play();
            Debug.Log("Rock Entered!");
        }
    }
}
