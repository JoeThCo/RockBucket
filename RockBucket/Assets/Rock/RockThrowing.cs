using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowing : MonoBehaviour
{
    public bool CanThrowRock { get; private set; } = true;

    [SerializeField] private Transform Eyes;
    [SerializeField] private Rigidbody rb;
    [Space(10)]
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
        newRock.Throw(Eyes.forward, rb.velocity.magnitude + 20);
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