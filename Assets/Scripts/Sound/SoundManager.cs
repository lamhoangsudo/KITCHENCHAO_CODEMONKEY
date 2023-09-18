using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private const string SOUND_EFFECT_VOLUME = "SoundEffectVolume";
    private float volume;
    public static SoundManager soundManager;
    private void Awake()
    {
        soundManager = this;
        volume = PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME);
    }
    private void Start()
    {
        DeliveryManager.deliveryManager.OnCompleteRecipe += DeliveryManager_OnCompleteRecipe;
        DeliveryManager.deliveryManager.OnFailedRecipe += DeliveryManager_OnFailedRecipe;
        CuttingCounter.OnPlayCuttingSound += CuttingCounter_OnPlayCuttingSound;
        PlayerController.player.OnPickSomeThing += Player_OnPickSomeThing;
        BaseCounter.OnAnyObjPlaceHere += BaseCounter_OnAnyObjPlaceHere;
        TrashCounter.OnTrashSound += TrashCounter_OnTrashSound;
    }
    private void OnDestroy()
    {
        CuttingCounter.OnPlayCuttingSound -= CuttingCounter_OnPlayCuttingSound;
        BaseCounter.OnAnyObjPlaceHere -= BaseCounter_OnAnyObjPlaceHere;
        TrashCounter.OnTrashSound -= TrashCounter_OnTrashSound;
    }

    private void TrashCounter_OnTrashSound(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position, volume);
    }

    private void BaseCounter_OnAnyObjPlaceHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objDrop, baseCounter.transform.position, volume);
    }

    private void Player_OnPickSomeThing(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objPickUp, PlayerController.player.transform.position, volume);
    }

    private void CuttingCounter_OnPlayCuttingSound(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position, volume);
    }

    private void DeliveryManager_OnFailedRecipe(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, DeliveryManager.deliveryManager.transform.position, volume);
    }

    private void DeliveryManager_OnCompleteRecipe(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryManager.deliveryManager.transform.position, volume);
    }

    private void PlaySound(AudioClip[] audioClip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip[Random.Range(0, audioClip.Length)], position, volume);
    }
    public void PlayeCountDownSound()
    {
        PlaySound(audioClipRefsSO.waring, Vector3.zero, volume);
    }
    public void PLaySoundBurnWarning(Vector3 position)
    {
        PlaySound(audioClipRefsSO.waring, position, volume);
    }
    public void PLaySoundWalking(Vector3 position)
    {
        PlaySound(audioClipRefsSO.footStep, position, volume);
    }

    public void ChangeSoundVolume()
    {
        volume += 0.1f;
        if (volume >= 1)
        {
            volume = 0;
        }
        PlayerPrefs.SetFloat(SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetSoundVolume()
    {
        return volume;
    }
}
