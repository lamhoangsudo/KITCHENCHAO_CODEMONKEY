using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static MusicManager musicManager;
    private const string MUSIC_VOLUME = "MusicVolume";
    private float volume;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        musicManager = this;
        volume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.5f);
        audioSource.volume = volume;
    }
    public void ChangeMusicVolume()
    {
        volume += 0.1f;
        if (volume >= 1)
        {
            volume = 0;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetMusicVolume()
    {
        return volume;
    }
}
