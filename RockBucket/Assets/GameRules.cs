using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Rules", fileName = "Audio")]
public class GameRules : ScriptableObject
{
    [Header("Wind")]
    [Range(15, 90)] public int WindChangeTime = 45;
    [Range(5, 50)] public int MaxWindStrength = 15;

    [Header("Player")]
    [Range(25, 150)] public int MaxThrowPower = 75;
}
