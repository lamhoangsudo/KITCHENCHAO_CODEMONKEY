using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualSelect;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.player.OnSelectCounterChange += Player_OnSelectCounterChange;
    }

    private void Player_OnSelectCounterChange(object sender, PlayerController.OnSelectCounterChangeEventArg e)
    {
        if (e.selectCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject gameObject in visualSelect)
        {
            gameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject gameObject in visualSelect)
        {
            gameObject.SetActive(false);
        }
    }
}
