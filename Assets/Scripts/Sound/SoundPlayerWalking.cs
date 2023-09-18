using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerWalking : MonoBehaviour
{
    private PlayerController player;
    private float timeFootStep;
    [SerializeField] private float timeFoodStepMax;
    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    private void Update()
    {
        timeFootStep -= Time.deltaTime;
        if (timeFootStep <= 0)
        {
            timeFootStep = timeFoodStepMax;
            if(player.IsWalking())
            {
                SoundManager.soundManager.PLaySoundWalking(player.transform.position);
            }
        }
    }
}
