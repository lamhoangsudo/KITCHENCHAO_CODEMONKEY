using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelivevyCounter : BaseCounter
{
    public static DelivevyCounter delivevyCounter { get; private set; }
    private void Awake()
    {
        delivevyCounter = this;
    }
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObj())
        {
            if(player.GetKitchenObj().GetPlateKitchenObj(out PlateKitchenObj plateKitchenObj))
            {
                DeliveryManager.deliveryManager.DeLiveryFood(plateKitchenObj);
                player.GetKitchenObj().DestroyKitchenObj();
            }
        }
    }
}
