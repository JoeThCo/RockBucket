using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySound(AudioScriptableObject audioSO)
    {
        audioSource.clip = audioSO.Clip;

        audioSource.pitch = audioSO.GetRandomPitch();
        audioSource.volume = audioSO.Volume;

        audioSource.Play();
        Destroy(gameObject, audioSO.Clip.length);
    }

    public void PlaySound(AudioScriptableObject audioSO, float pitch = 1)
    {
        audioSource.clip = audioSO.Clip;

        audioSource.pitch = pitch;
        audioSource.volume = audioSO.Volume;

        audioSource.Play();
        Destroy(gameObject, audioSO.Clip.length);
    }
}
