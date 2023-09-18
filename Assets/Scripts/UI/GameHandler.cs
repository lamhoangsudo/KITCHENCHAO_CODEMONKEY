using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class GameHandler : MonoBehaviour
{
    private enum State
    {
        WattingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    private State state;
    [SerializeField] private float timeCountDownToStart = 3f;
    [SerializeField] private float timeGamePlaying = 10f;
    private float timePlay;
    //this static will destroy when this obj destroy
    //not call error
    public static GameHandler gameHandler;
    public event EventHandler OnStateChange;
    public event EventHandler<bool> OnPauseUI;
    public event EventHandler<bool> OnTutorialUI;
    private bool pause = false;
    private bool tutorial = true;
    private void Awake()
    {
        gameHandler = this;
        state = State.WattingToStart;
        timePlay = timeGamePlaying;
    }
    private void Start()
    {
        GameInput.gameInput.OnPause += GameInput_OnPause;
        GameInput.gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        OnTutorial();
    }

    private void GameInput_OnPause(object sender, EventArgs e)
    {
        OnPause();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WattingToStart:
                Time.timeScale = 0f;
                break;
            case State.CountDownToStart:
                timeCountDownToStart -= Time.deltaTime;
                if (timeCountDownToStart <= 0f)
                {
                    state = State.GamePlaying;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                timeGamePlaying -= Time.deltaTime;
                if (timeGamePlaying <= 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }
    public bool OnPLay()
    {
        return state == State.GamePlaying;
    }
    public bool OnCountDown()
    {
        return state == State.CountDownToStart;
    }
    public bool OnOver()
    {
        return state == State.GameOver;
    }
    public float CountDownTimer()
    {
        return timeCountDownToStart;
    }
    public float timePlayNomolize()
    {
        return timeGamePlaying / timePlay;
    }
    public void OnPause()
    {
        if (state != State.GameOver)
        {
            if (pause == false)
            {
                Time.timeScale = 0f;
                pause = true;
                OnPauseUI?.Invoke(this, pause);
            }
            else
            {
                Time.timeScale = 1f;
                pause = false;
                OnPauseUI?.Invoke(this, pause);
            }
        }
    }
    public void OnTutorial()
    {
        if (state == State.WattingToStart)
        {
            Time.timeScale = 1f;
            tutorial = false;
            state = State.CountDownToStart;
            OnStateChange?.Invoke(this, EventArgs.Empty);
            OnTutorialUI?.Invoke(this, tutorial);
        }
    }
}
