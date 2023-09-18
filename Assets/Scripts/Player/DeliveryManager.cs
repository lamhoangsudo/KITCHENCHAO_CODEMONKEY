using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField]
    private ListFoodOrderSO listFoodOrderSO;
    private List<FoodRecipeSo> waitingFoodRecipes;
    private float timeOrderSpawn;
    [SerializeField] private float timeMaxOrderSpawn = 4f;
    [SerializeField] private int maxOrderHas = 4;
    public event EventHandler OnRecipeSpawned;
    //event to delete recipe has delivery completed
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnCompleteRecipe;
    public event EventHandler OnFailedRecipe;
    private int numberDelivery;
    public static DeliveryManager deliveryManager { get; private set; }
    private void Awake()
    {
        deliveryManager = this;
        waitingFoodRecipes = new List<FoodRecipeSo>();
    }
    private void Update()
    {
        if (GameHandler.gameHandler.OnPLay())
        {
            timeOrderSpawn -= Time.deltaTime;
            if (timeOrderSpawn <= 0)
            {
                timeOrderSpawn = timeMaxOrderSpawn;
                if (waitingFoodRecipes.Count < maxOrderHas)
                {
                    FoodRecipeSo watingFoodRecipeSo = listFoodOrderSO.FoodRecipeListSo[UnityEngine.Random.Range(0, listFoodOrderSO.FoodRecipeListSo.Count)];
                    waitingFoodRecipes.Add(watingFoodRecipeSo);
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    public void DeLiveryFood(PlateKitchenObj plateKitchenObj)
    {
        bool playerDeliveryMatchFoodOrder = false;
        foreach (FoodRecipeSo foodRecipeSo in waitingFoodRecipes)
        {
            if (plateKitchenObj.GetListKitchenObjSO().Count == foodRecipeSo.kitchenObjSOList.Count)
            {
                foreach (KitchenObjSO recipeFoodOrder in foodRecipeSo.kitchenObjSOList)
                {
                    bool foodMatchVsPlate = false;
                    foreach (KitchenObjSO recipeFoodPlayerMake in plateKitchenObj.GetListKitchenObjSO())
                    {
                        if (recipeFoodOrder == recipeFoodPlayerMake)
                        {
                            foodMatchVsPlate = true;
                            break;
                        }
                    }
                    if (!foodMatchVsPlate)
                    {
                        playerDeliveryMatchFoodOrder = false;
                        break;
                    }
                    else
                    {
                        playerDeliveryMatchFoodOrder = true;
                    }
                    if (playerDeliveryMatchFoodOrder == true)
                    {
                        waitingFoodRecipes.Remove(foodRecipeSo);
                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                        OnCompleteRecipe?.Invoke(this, EventArgs.Empty);
                        numberDelivery++;
                        Debug.Log("Match");
                        return;
                    }
                }
            }
            else
            {
                playerDeliveryMatchFoodOrder = false;
            }
        }
        if (playerDeliveryMatchFoodOrder == false)
        {
            OnFailedRecipe?.Invoke(this, EventArgs.Empty);
            Debug.Log("NotMatch");
            return;
        }
    }
    public List<FoodRecipeSo> GetWaitingFoodRecipes()
    {
        return waitingFoodRecipes;
    }
    public int GetNumberDelivery()
    {
        return numberDelivery;
    }
}
