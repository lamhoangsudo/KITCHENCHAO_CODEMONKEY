using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class ListFoodOrderSO : ScriptableObject
{
    public List<FoodRecipeSo> FoodRecipeListSo;
    public float Time;
}
