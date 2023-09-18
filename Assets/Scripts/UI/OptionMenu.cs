using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button moveUpBtn;
    [SerializeField] private Button moveDownBtn;
    [SerializeField] private Button moveLeftBtn;
    [SerializeField] private Button moveRightBtn;
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button interactAlternateBtn;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpTxt;
    [SerializeField] private TextMeshProUGUI moveDownTxt;
    [SerializeField] private TextMeshProUGUI moveLeftTxt;
    [SerializeField] private TextMeshProUGUI moveRightTxt;
    [SerializeField] private TextMeshProUGUI interactTxt;
    [SerializeField] private TextMeshProUGUI interactAlternateTxt;
    [SerializeField] private TextMeshProUGUI pauseTxt;
    [SerializeField] private Transform keyUI;
    public static OptionMenu optionMenu;
    private Action OnPauseMenu;
    private void Awake()
    {
        optionMenu = this;
        GameHandler.gameHandler.OnPauseUI += GameHandler_OnPauseUI;
        Hide();
    }
    //Hide UI when player press esc
    private void GameHandler_OnPauseUI(object sender, bool e)
    {
        //catch when player press esc again show pause UI not show option UI
        if (e != true)
        {
            Hide();
        }
    }

    private void Start()
    {
        soundBtn.onClick.AddListener(() =>
        {
            SoundManager.soundManager.ChangeSoundVolume();
        });
        musicBtn.onClick.AddListener(() =>
        {
            MusicManager.musicManager.ChangeMusicVolume();
        });
        backBtn.onClick.AddListener(() =>
        {         
            OnPauseMenu();
            Hide();
        });
        moveUpBtn.onClick.AddListener(() =>
        {
            UpdateKeyControl(GameInput.Binding.MoveUp);
        });
        moveDownBtn.onClick.AddListener(() =>
        {
            UpdateKeyControl(GameInput.Binding.MoveDown);
        });
        moveLeftBtn.onClick.AddListener(() =>
        {
            UpdateKeyControl(GameInput.Binding.MoveLeft);
        });
        moveRightBtn.onClick.AddListener(() =>
        {
            UpdateKeyControl(GameInput.Binding.MoveRight);
        });
        interactBtn.onClick.AddListener(() =>
        {
            UpdateKeyControl(GameInput.Binding.Interact);
        });
        interactAlternateBtn.onClick.AddListener(() =>
        {
            UpdateKeyControl(GameInput.Binding.InteractAlt);
        });
        pauseBtn.onClick.AddListener(() =>
        {
            UpdateKeyControl(GameInput.Binding.Pause);
        });
        UpdateVisual(); 
        HideKeyUI();
    }
    private void Update()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        soundText.text = "SoundEffect: " + Mathf.Round(SoundManager.soundManager.GetSoundVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.musicManager.GetMusicVolume() * 10f);
        moveUpTxt.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveUp);
        moveDownTxt.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveDown);
        moveLeftTxt.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveLeft);
        moveRightTxt.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveRight);
        interactTxt.text = GameInput.gameInput.GetBinding(GameInput.Binding.Interact);
        interactAlternateTxt.text = GameInput.gameInput.GetBinding(GameInput.Binding.InteractAlt);
        pauseTxt.text = GameInput.gameInput.GetBinding(GameInput.Binding.Pause);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show(Action OnPauseMenu)
    {
        soundBtn.Select();
        this.OnPauseMenu = OnPauseMenu;
        gameObject.SetActive(true);
    }
    private void HideKeyUI()
    {
        keyUI.gameObject.SetActive(false);
    }
    private void ShowKeyUI()
    {
        keyUI.gameObject.SetActive(true);
    }
    private void UpdateKeyControl(GameInput.Binding binding)
    {
        ShowKeyUI();
        GameInput.gameInput.RebindBinding(binding, HideKeyUI);
    }
}
