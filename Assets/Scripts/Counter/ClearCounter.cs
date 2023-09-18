using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    //[SerializeField] private KitchenObjSO kitchenObjSO;
    public override void Interact(PlayerController player)
    {
        if (HasKitchenObj() == false)
        {
            if(player.HasKitchenObj())
            {
                //go to counter
                player.GetKitchenObj().SetKitchenObjParent(this);
            }
            else
            {
                //do not thing
            }
        }
        else
        {
            if (player.HasKitchenObj())
            {
                if(player.GetKitchenObj().GetPlateKitchenObj(out PlateKitchenObj plateKitchenObj))
                {
                    if(plateKitchenObj.TryAddIngredient(GetKitchenObj().GetKitchenObjSO()))
                    {
                        GetKitchenObj().DestroyKitchenObj();
                    }                    
                }
                else
                {
                    if(this.GetKitchenObj().GetPlateKitchenObj(out plateKitchenObj))
                    {
                        if(plateKitchenObj.TryAddIngredient(player.GetKitchenObj().GetKitchenObjSO()))
                        {
                            player.GetKitchenObj().DestroyKitchenObj();
                        }
                    }
                }
            }
            else
            {
                //go to player
                GetKitchenObj().SetKitchenObjParent(player);
            }
        }
    }
}
