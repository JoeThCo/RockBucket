using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio ScriptableObject", fileName = "Audio")]
public class AudioScriptableObject : ScriptableObject
{
    public AudioClip Clip;
    [Space(10)]

    [Range(.1f, 1f)]
    [SerializeField] public float Volume = 1;

    [SerializeField][Range(-0.25f, 0f)] private float frequencyMin = -0.25f;
    [SerializeField][Range(0f, 0.25f)] private float frequencyMax = 0.25f;

    public float GetRandomPitch()
    {
        return 1 + Random.Range(frequencyMin, frequencyMax);
    }
}