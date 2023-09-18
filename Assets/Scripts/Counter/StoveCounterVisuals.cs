using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisuals : MonoBehaviour
{
    [SerializeField] private GameObject SizzlingParticles;
    [SerializeField] private GameObject StoveOnVisual;
    [SerializeField] private StoveCounter stoveCounter;
    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArg e)
    {
        bool check = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (check)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        SizzlingParticles.SetActive(false);
        StoveOnVisual.SetActive(false);
    }
    private void Show()
    {
        SizzlingParticles.SetActive(true);
        StoveOnVisual.SetActive(true);
    }
}
