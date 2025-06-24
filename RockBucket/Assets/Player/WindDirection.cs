using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDirection : MonoBehaviour
{
    [SerializeField] private ParticleSystem windParticles;

    private float windStrength;
    private float time = 0;

    private bool isNoWind = false;
    private bool playSFX = false;

    public delegate void WindEvent();
    public event WindEvent WindChanged;
    public event WindEvent WindChangedUI;

    public Vector3 Direction { get { return windParticles.transform.forward; } }
    public float WindSpeed01 { get { return windStrength / GameManager.Instance.GameRules.MaxWindStrength; } }
    private float WindSpeed { get { return windStrength * 2f; } }
    public string WindSpeedString { get { return $"{WindSpeed.ToString("F0")} mph"; } }

    private const float NO_WIND = 0.01f;

    private void Awake()
    {
        WindChanged += WindDirection_WindChanged;
    }

    private void Start()
    {
        WindDirection_WindChanged();
        playSFX = true;
    }

    private void OnDisable()
    {
        WindChanged -= WindDirection_WindChanged;
    }

    private void FixedUpdate()
    {
        if (time < GameManager.Instance.GameRules.WindChangeTime)
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
        windStrength = isNoWind ? NO_WIND : Random.value * GameManager.Instance.GameRules.MaxWindStrength;

        transform.localRotation = Quaternion.Euler(0, Random.value * 360, 0);

        ParticleSystem.EmissionModule emissionModule = windParticles.emission;
        emissionModule.rateOverTime = (WindSpeed01 * 5) + 1;

        ParticleSystem.MainModule mainModule = windParticles.main;
        mainModule.startSpeed = WindSpeed01 * 1.5f;

        isNoWind = !isNoWind;

        if (playSFX)
            SoundEffectController.Play("WindChanged");

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