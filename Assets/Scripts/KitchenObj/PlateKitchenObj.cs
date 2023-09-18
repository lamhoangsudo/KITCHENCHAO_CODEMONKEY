using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObj : KitchenObj
{
    private List<KitchenObjSO> kitchenObjList;
    [SerializeField] private List<KitchenObjSO> validateKitchenObjList;
    public event EventHandler<OnIngredientAddEventArgs> OnIngredientAdd;
    public class OnIngredientAddEventArgs : EventArgs
    {
        public KitchenObjSO KitchenObjSO;
    }
    private void Awake()
    {
        kitchenObjList = new List<KitchenObjSO>();
    }
    public bool TryAddIngredient(KitchenObjSO kitchenObjSO)
    {
        if(kitchenObjList.Contains(kitchenObjSO) || validateKitchenObjList.Contains(kitchenObjSO))
        {
            return false;
        }
        else 
        {
            kitchenObjList.Add(kitchenObjSO);
            OnIngredientAdd?.Invoke(this, new OnIngredientAddEventArgs
            {
                KitchenObjSO = kitchenObjSO
            });
            return true; 
        }
    }
    public List<KitchenObjSO> GetListKitchenObjSO()
    {
        return kitchenObjList;
    } 
}
