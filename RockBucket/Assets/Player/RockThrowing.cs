using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RockThrowing : MonoBehaviour
{
    public bool CanThrowRock { get; private set; } = true;

    [SerializeField] private Transform Camera;
    [Space(10)]
    [SerializeField] private Rigidbody rb;
    [Space(10)]
    [SerializeField][Range(10, 100)] private int MaxThrow = 50;
    [SerializeField] private Rock rockPrefab;
    [Space(10)]
    [SerializeField][Range(0f, 0.5f)] private float pitchOffset;
    [SerializeField] private AudioSource rockPowerAudioSource;
    [SerializeField] private PowerBarUI powerBarUI;

    public float Power { get { return power; } }
    private float power = 0;
    private float throwStartTime = 0;

    public delegate void ThrowEvent();

    public event ThrowEvent ThrowStart;
    public event ThrowEvent ThrowMiddle;
    public event ThrowEvent ThrowEnd;

    private void Awake()
    {
        ThrowStart += RockThrowing_ThrowStart;
        ThrowMiddle += RockThrowing_ThrowMiddle;
        ThrowEnd += RockThrowing_ThrowEnd;
    }

    private void OnDisable()
    {
        ThrowStart -= RockThrowing_ThrowStart;
        ThrowMiddle -= RockThrowing_ThrowMiddle;
        ThrowEnd -= RockThrowing_ThrowEnd;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() || GameManager.IsPaused || !CanThrowRock) return;

        if (Input.GetMouseButtonDown(0))
            ThrowStart?.Invoke();

        if (Input.GetMouseButton(0))
            ThrowMiddle?.Invoke();

        if (Input.GetMouseButtonUp(0))
            ThrowEnd.Invoke();
    }

    private void RockThrowing_ThrowStart()
    {
        throwStartTime = Time.time;
        power = 0;
        rockPowerAudioSource.Play();
    }

    private void RockThrowing_ThrowMiddle()
    {
        power = Mathf.PingPong(Time.time - throwStartTime, 1);
        rockPowerAudioSource.pitch = power;
    }

    private void RockThrowing_ThrowEnd()
    {
        SoundEffectController.Play("Throw", Camera.position);
        throwRock();
        power = 0;
        rockPowerAudioSource.Stop();
    }

    private void throwRock()
    {
        Rock newRock = Instantiate(rockPrefab, Camera.position, Quaternion.Euler(Camera.forward), null);
        newRock.Throw(Camera.forward, Mathf.Sqrt(rb.velocity.magnitude) + (MaxThrow * power));
        newRock.RockDelete += OnRockDelete;
        CanThrowRock = false;
    }

    private void OnRockDelete(Rock rock)
    {
        rock.RockDelete -= OnRockDelete;
        rock.DeleteRock();
        CanThrowRock = true;
    }
}