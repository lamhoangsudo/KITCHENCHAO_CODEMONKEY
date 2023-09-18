using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class FoodRecipeSo : ScriptableObject
{
    public List<KitchenObjSO> kitchenObjSOList;
    public string Name;
    public float Time;
}
