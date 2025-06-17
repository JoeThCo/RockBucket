using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] private float maxWindStrength = 25;
    private float windStrength;

    [Space(10)]
    [SerializeField] float changeWindTime = 5;
    private float time = 0;

    public delegate void WindEvent();
    public WindEvent WindChanged;

    private void Start()
    {
        windChanged();
        WindChanged += windChanged;
    }

    private void OnDisable()
    {
        WindChanged -= windChanged;
    }

    private void FixedUpdate()
    {
        if (time < changeWindTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            WindChanged?.Invoke();
            time = 0;
        }
    }

    private void windChanged()
    {
        transform.localRotation = Quaternion.Euler(0, Random.value * 360, 0);
        windStrength = Random.value * maxWindStrength;
    }

    private void OnTriggerStay(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock == null) return;

        rock.ApplyWind(transform.forward, windStrength);
    }
}