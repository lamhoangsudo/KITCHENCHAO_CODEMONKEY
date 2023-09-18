using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image backGroudColor;
    [SerializeField] private Image icon;
    [SerializeField] private Color colorSUCCESS;
    [SerializeField] private Color colorFAIL;
    [SerializeField] private Sprite spriteSUCCESS;
    [SerializeField] private Sprite spriteFAIL;
    private Animator animator;
    private const string ON_TRIGGER = "OnDelivery";
    private const string SUCCESS = "Delivery \r\nSuccess";
    private const string FAIL = "Delivery \r\nFail";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        deliveryManager.OnCompleteRecipe += DeliveryManager_OnCompleteRecipe;
        deliveryManager.OnFailedRecipe += DeliveryManager_OnFailedRecipe;
        Hide();
    }

    private void DeliveryManager_OnFailedRecipe(object sender, System.EventArgs e)
    {
        Show();
        text.text = FAIL;
        backGroudColor.color = colorFAIL;
        icon.sprite = spriteFAIL;

        animator.SetTrigger(ON_TRIGGER);

    }

    private void DeliveryManager_OnCompleteRecipe(object sender, System.EventArgs e)
    {
        Show();
        text.text = SUCCESS;
        backGroudColor.color = colorSUCCESS;
        icon.sprite = spriteSUCCESS;
        animator.SetTrigger(ON_TRIGGER);

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
