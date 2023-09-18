using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class KitchenRecipeSO : ScriptableObject
{
    public KitchenObjSO input;
    public KitchenObjSO output;
    public int cuttingProcess;
}
