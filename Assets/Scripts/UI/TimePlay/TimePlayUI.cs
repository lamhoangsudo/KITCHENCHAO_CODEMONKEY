using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePlayUI : MonoBehaviour
{
    [SerializeField] private Image timeLeft;
    private void Start()
    {
        Hide();
        timeLeft.fillAmount = 1f;
        GameHandler.gameHandler.OnStateChange += GameHandler_OnStateChange;
    }

    private void GameHandler_OnStateChange(object sender, System.EventArgs e)
    {
        if(GameHandler.gameHandler.OnPLay())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        timeLeft.fillAmount = GameHandler.gameHandler.timePlayNomolize();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
