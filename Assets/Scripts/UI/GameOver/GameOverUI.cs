using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberDelivery;
    private void Start()
    {
        GameHandler.gameHandler.OnStateChange += GameHandler_OnStateChange;
        Hide();
    }
    private void Update()
    {
        numberDelivery.text = DeliveryManager.deliveryManager.GetNumberDelivery().ToString();
    }
    private void GameHandler_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.gameHandler.OnOver())
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
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
