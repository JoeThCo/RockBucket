using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBucket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rock rock = other.gameObject.GetComponent<Rock>();
        if (rock != null) 
        {
            Debug.Log("Rock Entered!");
        }
    }
}
