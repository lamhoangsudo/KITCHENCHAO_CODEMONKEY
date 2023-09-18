using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private float timeToShowWarning;
    [SerializeField] private Image image;
    public bool OnActive { get; private set; }
    private void Start()
    {
        stoveCounter.OnProcessBar += StoveCounter_OnProcessBar;
        Hide();
        OnActive = false;
    }

    private void StoveCounter_OnProcessBar(object sender, IHasProcess.OnPrecessChangerEventArgs e)
    {
        if (stoveCounter.IsFried() && e.processNormalized > timeToShowWarning)
        {
            Show();
            OnActive = true;
        }
        else
        {
            Hide();
            OnActive = false;
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
