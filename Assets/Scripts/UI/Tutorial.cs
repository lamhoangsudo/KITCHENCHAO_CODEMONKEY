using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI move_Up;
    [SerializeField] private TextMeshProUGUI move_Down;
    [SerializeField] private TextMeshProUGUI move_Left;
    [SerializeField] private TextMeshProUGUI move_Right;
    [SerializeField] private TextMeshProUGUI interact;
    [SerializeField] private TextMeshProUGUI interactAlt;
    [SerializeField] private TextMeshProUGUI pause;
    public static Tutorial tutorial;
    private void Start()
    {
        Show();
        tutorial = this;
        UpdateVisual();
        GameHandler.gameHandler.OnTutorialUI += GameHandler_OnTutorialUI;
    }

    private void GameHandler_OnTutorialUI(object sender, bool e)
    {
        if(e == false)
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
    private void UpdateVisual()
    {
        move_Up.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveUp);
        move_Down.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveDown);
        move_Left.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveLeft);
        move_Right.text = GameInput.gameInput.GetBinding(GameInput.Binding.MoveRight);
        interact.text = GameInput.gameInput.GetBinding(GameInput.Binding.Interact);
        interactAlt.text = GameInput.gameInput.GetBinding(GameInput.Binding.InteractAlt);
        pause.text = GameInput.gameInput.GetBinding(GameInput.Binding.Pause);
    }
}
