using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Rock rock = collision.gameObject.GetComponent<Rock>();
        if (rock == null) return;
        SoundEffectController.Play("BucketHit", rock.transform.position);
    }
}