using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image image;
    public void SetIcon(KitchenObjSO kitchenObjSO)
    {
        image.sprite = kitchenObjSO.sprite;
    }
}
