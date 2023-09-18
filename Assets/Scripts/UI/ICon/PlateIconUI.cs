using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObj plateKitchenObj;
    [SerializeField] private Transform icon;
    private void Awake()
    {
        icon.gameObject.SetActive(false);
    }
    void Start()
    {
        plateKitchenObj.OnIngredientAdd += PlateKitchenObj_OnIngredientAdd;
    }

    private void PlateKitchenObj_OnIngredientAdd(object sender, PlateKitchenObj.OnIngredientAddEventArgs e)
    {
        UpdateIcon();
    }
    public void UpdateIcon()
    {
        foreach (Transform child in transform)
        {
            if (child == icon) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjSO kitchenObjSO in plateKitchenObj.GetListKitchenObjSO())
        {
            Transform iconTransform = Instantiate(icon, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetIcon(kitchenObjSO);
        }
    }
}
