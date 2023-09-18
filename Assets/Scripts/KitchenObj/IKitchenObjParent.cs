using System.Diagnostics.PerformanceData;
using UnityEngine;

public interface IKitchenObjParent
{
    public Transform GetCounterTop();
    public KitchenObj GetKitchenObj();
    public void SetKitchenObj(KitchenObj kitchenObj);
    public void ClearKitchenObj();
    public bool HasKitchenObj();
}