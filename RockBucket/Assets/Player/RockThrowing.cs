using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private PowerBarUI powerBarUI;
    private float power = 0;
    private float throwStartTime = 0;

    public delegate void ThrowEvent();
    public delegate void PoweredThrowEvent(float t);

    public ThrowEvent ThrowStart;
    public PoweredThrowEvent ThrowMiddle;
    public ThrowEvent ThrowEnd;

    private void Awake()
    {
        ThrowStart += throwStart;
        ThrowMiddle += throwMiddle;
        ThrowEnd += throwEnd;
    }

    private void OnDisable()
    {
        ThrowStart -= throwStart;
        ThrowMiddle -= throwMiddle;
        ThrowEnd -= throwEnd;
    }

    private void Update()
    {
        if (!CanThrowRock) return;

        if (Input.GetMouseButtonDown(0))
            ThrowStart?.Invoke();

        if (Input.GetMouseButton(0))
            ThrowMiddle?.Invoke(power);

        if (Input.GetMouseButtonUp(0))
            ThrowEnd.Invoke();
    }

    private void throwStart()
    {
        throwStartTime = Time.time;
        power = 0;
    }

    private void throwMiddle(float _)
    {
        power = Mathf.PingPong(Time.time - throwStartTime, 1);
    }

    private void throwEnd()
    {
        SoundEffectController.Play("Throw", Camera.position);
        throwRock();
        power = 0;
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