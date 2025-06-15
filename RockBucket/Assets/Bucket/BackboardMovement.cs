using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackboardMovement : MonoBehaviour
{
    public Transform player;
    public Transform circleCenter;
    public float circleRadius = 2f;

    private float startY;
    private void Start()
    {
        startY = transform.position.y;
    }

    void FixedUpdate()
    {
        if (player == null || circleCenter == null) return;

        Vector3 toPlayer = player.position - circleCenter.position;
        toPlayer.Normalize();

        Vector3 oppositePosition = circleCenter.position + (-toPlayer * circleRadius);
        oppositePosition.y = startY;

        transform.position = oppositePosition;

        Vector3 rotation = Quaternion.LookRotation(-toPlayer).eulerAngles;
        rotation.x = 0;
        rotation.y = rotation.y + 180;

        transform.rotation = Quaternion.Euler(rotation);
    }
}