using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainCounter : BaseCounter
{
    // Start is called before the first frame update
    public event EventHandler OnPlayerGrabbedObj;
    [SerializeField] private KitchenObjSO kitchenObjSO;
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObj() == false)
        {
            KitchenObj.SpawnKitchenObj(kitchenObjSO, player);
            OnPlayerGrabbedObj?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log(player.GetKitchenObj().name);
            Debug.Log(this.kitchenObjSO.name);
            if (player.GetKitchenObj().name.Trim().Contains(this.kitchenObjSO.name.Trim()))
            {
                Debug.Log(player.GetKitchenObj().name);
                Debug.Log(this.kitchenObjSO.name);
                player.GetKitchenObj().SetKitchenObjParent(this);
                player.ClearKitchenObj();
                this.GetKitchenObj().DestroyKitchenObj();
                OnPlayerGrabbedObj?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
