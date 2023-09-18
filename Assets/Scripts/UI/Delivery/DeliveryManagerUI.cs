using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        UpdateVisual();
        DeliveryManager.deliveryManager.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.deliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;       
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(FoodRecipeSo foodRecipeSo in DeliveryManager.deliveryManager.GetWaitingFoodRecipes())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.GetComponent<DeliverySingerUI>().SetNameRecepi(foodRecipeSo);
            recipeTransform.gameObject.SetActive(true);
        }
    }
}
