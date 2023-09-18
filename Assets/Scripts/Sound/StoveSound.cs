using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    [SerializeField] private StoveBurnWarningUI warningUI;
    [SerializeField] private float maxTimePlay;
    private float timePlay;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }
    private void Update()
    {
        if(warningUI.OnActive == true)
        {
            timePlay -= Time.deltaTime;
            if(timePlay <= 0)
            {
                timePlay = maxTimePlay;
                SoundManager.soundManager.PLaySoundBurnWarning(stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArg e)
    {
        bool play = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (play)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
