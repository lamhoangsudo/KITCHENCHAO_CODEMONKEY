using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Security.Permissions;
using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    [SerializeField] private KitchenObjSO KitchenObjSO;
    private IKitchenObjParent IKitchenParent;
    public KitchenObjSO GetKitchenObjSO()
    {
        return KitchenObjSO;
    }
    public void SetKitchenObjParent(IKitchenObjParent kitchenParent)
    {
        if (this.IKitchenParent != null)
        {
            this.IKitchenParent.ClearKitchenObj();
        }
        this.IKitchenParent = kitchenParent;
        kitchenParent.SetKitchenObj(this);
        transform.parent = kitchenParent.GetCounterTop();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjParent GetIKitchenParent()
    {
        return IKitchenParent;
    }

    public void DestroyKitchenObj()
    {
        this.GetIKitchenParent().ClearKitchenObj();
        Destroy(gameObject, 0);
    }
    public static KitchenObj SpawnKitchenObj(KitchenObjSO kitchenObjSO, IKitchenObjParent kitchenObjParent)
    {
        Transform kitchenObjTransform = Instantiate(kitchenObjSO.prefab);
        KitchenObj kitchenObj = kitchenObjTransform.GetComponent<KitchenObj>();
        kitchenObj.SetKitchenObjParent(kitchenObjParent);
        return kitchenObj;
    }
    public bool GetPlateKitchenObj(out PlateKitchenObj plateKitchenObj)
    {
        if(this is PlateKitchenObj)
        {
            plateKitchenObj = this as PlateKitchenObj;
            return true;
        }
        else
        {
            plateKitchenObj = null;
            return false;
        }
    }
}
