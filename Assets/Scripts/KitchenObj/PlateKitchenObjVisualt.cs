using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObjVisualt : MonoBehaviour
{
    [SerializeField] private PlateKitchenObj plateKitchenObj;
    [Serializable]
    private struct KitchenObj_GameObj
    {
        public KitchenObjSO kitchenObjSO;
        public GameObject gameObj;
    }
    [SerializeField] private List<KitchenObj_GameObj> kitchenObj_GameObjList;
    private void Start()
    {
        plateKitchenObj.OnIngredientAdd += PlateKitchenObj_OnIngredientAdd;
        foreach (KitchenObj_GameObj kitchenObj_GameObj in kitchenObj_GameObjList)
        {
            kitchenObj_GameObj.gameObj.SetActive(false);
        }
    }

    private void PlateKitchenObj_OnIngredientAdd(object sender, PlateKitchenObj.OnIngredientAddEventArgs e)
    {
        foreach (KitchenObj_GameObj kitchenObj_GameObj in kitchenObj_GameObjList)
        {
            if (kitchenObj_GameObj.kitchenObjSO == e.KitchenObjSO)
            {
                kitchenObj_GameObj.gameObj.SetActive(true);
            }
        }
    }
}
