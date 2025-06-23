using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundEffectController
{
    private static AudioScriptableObject[] allSFX;
    private static SoundEffect soundEffectPrefab;

    public static void Load()
    {
        allSFX = Resources.LoadAll<AudioScriptableObject>("Audio/SFX");
        soundEffectPrefab = Resources.Load<SoundEffect>("Audio/SFX/SFX");
    }

    private static AudioScriptableObject Get(string name)
    {
        foreach (AudioScriptableObject obj in allSFX)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    public static void Play(string name, Vector3 position)
    {
        SoundEffect newSFX = GameObject.Instantiate(soundEffectPrefab, position, Quaternion.identity, null);
        AudioScriptableObject audioScriptableObject = Get(name);
        if (audioScriptableObject == null) return;

        newSFX.PlaySound(audioScriptableObject);
        GameObject.Destroy(newSFX, audioScriptableObject.Clip.length);
    }
}