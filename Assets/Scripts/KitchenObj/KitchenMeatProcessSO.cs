using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class KitchenMeatProcessSO : ScriptableObject
{
    public KitchenObjSO meatUnCook;
    public KitchenObjSO meatCook;
    public KitchenObjSO meatBurn;
    public float meatBurnTime;
    public float meatCookTime;
}
