using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowing : MonoBehaviour
{
    public bool CanThrowRock { get; private set; } = true;

    [SerializeField] private Transform Camera;
    [SerializeField] private Transform Eyes;
    [Space(10)]
    [SerializeField] private Rigidbody rb;
    [Space(10)]
    [SerializeField][Range(10, 100)] private int ThrowPower = 50;
    [SerializeField] private Rock rockPrefab;

    public delegate void OnRockThrown();
    public OnRockThrown RockThrown;

    private void Awake()
    {
        RockThrown += rockThrown;
    }

    private void OnDisable()
    {
        RockThrown -= rockThrown;
    }

    private void Update()
    {
        if (CanThrowRock && Input.GetMouseButtonDown(0))
            RockThrown.Invoke();
    }

    private void rockThrown()
    {
        Rock newRock = Instantiate(rockPrefab, Eyes.position, Quaternion.Euler(Eyes.forward), null);
        newRock.Throw(Camera.forward, Mathf.Sqrt(rb.velocity.magnitude) + ThrowPower);
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