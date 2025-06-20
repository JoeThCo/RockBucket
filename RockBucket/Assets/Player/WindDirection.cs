using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDirection : MonoBehaviour
{
    [SerializeField] private ParticleSystem windParticles;
    [SerializeField] private float maxWindStrength = 25;
    private float windStrength;

    [Space(10)]
    [SerializeField] float changeWindTime = 5;
    private float time = 0;

    public delegate void WindEvent();
    public event WindEvent WindChanged;
    public event WindEvent WindChangedUI;

    public Vector3 Direction { get { return windParticles.transform.forward; } }
    public float WindSpeed01 { get { return windStrength / maxWindStrength; } }
    private float WindSpeed { get { return windStrength * 2f; } }
    public string WindSpeedString { get { return $"{WindSpeed.ToString("F0")} mph"; } }

    private void Awake()
    {
        WindChanged += WindDirection_WindChanged;
    }

    private void Start()
    {
        WindDirection_WindChanged();
    }

    private void OnDisable()
    {
        WindChanged -= WindDirection_WindChanged;
    }

    private void FixedUpdate()
    {
        if (time < changeWindTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            if (WindChanged != null)
                WindChanged?.Invoke();

            time = 0;
        }
    }

    private void WindDirection_WindChanged()
    {
        transform.localRotation = Quaternion.Euler(0, Random.value * 360, 0);
        windStrength = Random.value * maxWindStrength;

        ParticleSystem.EmissionModule emissionModule = windParticles.emission;
        emissionModule.rateOverTime = (WindSpeed01 * 5) + 1;

        ParticleSystem.MainModule mainModule = windParticles.main;
        mainModule.startSpeed = WindSpeed01 * 1.5f;

        if (WindChangedUI != null)
            WindChangedUI?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock == null) return;

        rock.ApplyWind(transform.forward, windStrength);
    }
}