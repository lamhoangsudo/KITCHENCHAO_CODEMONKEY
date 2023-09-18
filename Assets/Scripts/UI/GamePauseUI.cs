using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button backMenu;
    [SerializeField] private Button resume;
    [SerializeField] private Button option;
    [SerializeField] private Transform optionsUI;
    private void Start()
    {
        Hide();
        GameHandler.gameHandler.OnPauseUI += GameHandler_OnPauseUI;
        backMenu.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MenuScenes);
        });
        resume.onClick.AddListener(() =>
        {
            GameHandler.gameHandler.OnPause();
        });
        option.onClick.AddListener(() =>
        {
            Hide();
            OptionMenu.optionMenu.Show(Show);           
        });
    }

    private void GameHandler_OnPauseUI(object sender, bool e)
    {
        if(e == true)
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
        resume.Select();
        gameObject.SetActive(true);
    }
}
