using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySingerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameRecipe;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetNameRecepi(FoodRecipeSo foodRecipeSo)
    {
        nameRecipe.text = foodRecipeSo.name; 
        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjSO kitchenObjSO in foodRecipeSo.kitchenObjSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.GetComponent<Image>().sprite = kitchenObjSO.sprite;
            iconTransform.gameObject.SetActive(true);
        }
    }
}
