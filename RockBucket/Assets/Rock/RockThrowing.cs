using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowing : MonoBehaviour
{
    public bool CanThrowRock { get; private set; } = true;

    [SerializeField] private Transform Eyes;
    [Space(10)]
    [SerializeField] private Rock rockPrefab;

    private void Update()
    {
        if (CanThrowRock)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Rock newRock = Instantiate(rockPrefab, Eyes.position, Quaternion.Euler(Eyes.forward), null);
                newRock.Throw(Eyes.forward, 20);
                newRock.RockStopped += OnRockStopped;

                CanThrowRock = false;
            }
        }
    }

    private void OnRockStopped(Rock rock)
    {
        CanThrowRock = true;
        rock.RockStopped -= OnRockStopped;
    }
}