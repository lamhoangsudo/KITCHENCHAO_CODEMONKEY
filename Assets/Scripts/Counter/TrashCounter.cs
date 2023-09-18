using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    [DefaultValue(null)]
    public static event EventHandler OnTrashSound;
    public override void Interact(PlayerController player)
    {
        if(player.HasKitchenObj())
        {
            player.GetKitchenObj().DestroyKitchenObj();
            OnTrashSound?.Invoke(this, EventArgs.Empty);
        }
    }
}
