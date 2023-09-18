using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.PerformanceData;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjParent
{
    [SerializeField] protected Transform counterTop;
    protected KitchenObj kitchenObj;
    [DefaultValue(null)]
    public static event EventHandler OnAnyObjPlaceHere;
    public virtual void Interact(PlayerController player) {}

    public virtual void InteractAlternate(PlayerController player)
    {

    }
    public Transform GetCounterTop()
    {
        return counterTop;
    }
    public KitchenObj GetKitchenObj()
    {
        return kitchenObj;
    }
    public void SetKitchenObj(KitchenObj kitchenObj)
    {
        this.kitchenObj = kitchenObj;
        if (kitchenObj != null)
        {
            OnAnyObjPlaceHere?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ClearKitchenObj()
    {
        kitchenObj = null;
    }
    public bool HasKitchenObj()
    {
        return kitchenObj != null;
    }
}
