using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDown;
    private Animator animator;
    private int lastNumber;
    private const string COUNTDOWN_ANIMATOR_TRIGGER = "CountDown";
    private void Awake()
    {
        animator = countDown.gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        GameHandler.gameHandler.OnStateChange += GameHandler_OnStateChange;       
        Hide();
    }
    private void Update()
    {
        int numberCountDown = Mathf.CeilToInt(GameHandler.gameHandler.CountDownTimer());
        if (lastNumber != numberCountDown)
        {
            lastNumber = numberCountDown;
            animator.SetTrigger(COUNTDOWN_ANIMATOR_TRIGGER);
            countDown.text = numberCountDown.ToString();
            SoundManager.soundManager.PlayeCountDownSound();
        }
        
    }
    private void GameHandler_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.gameHandler.OnCountDown())
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
