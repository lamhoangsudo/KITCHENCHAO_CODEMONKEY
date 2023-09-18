using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class CuttingCounter : BaseCounter, IKitchenObjParent, IHasProcess
{
    [SerializeField] private KitchenRecipeSO[] cuttingKitchenObjSOArray;
    public event EventHandler<IHasProcess.OnPrecessChangerEventArgs> OnProcessBar;
    public event EventHandler OnPrecessAminator;
    //this static will not destroy when tho screen change
    //bescause this static belong to event that why this will not destroy when screen change
    //cause error like player input
    [DefaultValue(null)]
    public static event EventHandler OnPlayCuttingSound;
    private int cuttingProcess;
    public override void Interact(PlayerController player)
    {
        if (HasKitchenObj() == false)
        {
            if (player.HasKitchenObj())
            {
                //go to counter
                if (GetOutputKitchenRecipeSO(player.GetKitchenObj().GetKitchenObjSO()) != null)
                {
                    player.GetKitchenObj().SetKitchenObjParent(this);
                    cuttingProcess = 0;
                    KitchenRecipeSO outputKitchenRecipeSO = GetOutputKitchenRecipeSO(this.GetKitchenObj().GetKitchenObjSO());
                    OnProcessBar?.Invoke(this, new IHasProcess.OnPrecessChangerEventArgs
                    {
                        processNormalized = (float)cuttingProcess / outputKitchenRecipeSO.cuttingProcess
                    });
                }
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
                if (player.GetKitchenObj().GetPlateKitchenObj(out PlateKitchenObj plateKitchenObj))
                {
                    if (plateKitchenObj.TryAddIngredient(GetKitchenObj().GetKitchenObjSO()))
                    {
                        GetKitchenObj().DestroyKitchenObj();
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

    public override void InteractAlternate(PlayerController player)
    {
        if (HasKitchenObj())
        {
            KitchenRecipeSO outputKitchenRecipeSO = GetOutputKitchenRecipeSO(this.GetKitchenObj().GetKitchenObjSO());
            if (outputKitchenRecipeSO != null)
            {
                cuttingProcess++;
                OnProcessBar?.Invoke(this, new IHasProcess.OnPrecessChangerEventArgs
                {
                    processNormalized = (float)cuttingProcess / outputKitchenRecipeSO.cuttingProcess
                });
                OnPrecessAminator?.Invoke(this, EventArgs.Empty);
                if (cuttingProcess == outputKitchenRecipeSO.cuttingProcess)
                {
                    this.GetKitchenObj().DestroyKitchenObj();
                    KitchenObj.SpawnKitchenObj(outputKitchenRecipeSO.output, player);
                }
                OnPlayCuttingSound?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private KitchenRecipeSO GetOutputKitchenRecipeSO(KitchenObjSO inputKitchenObjSO)
    {
        foreach(KitchenRecipeSO recipeSO in cuttingKitchenObjSOArray)
        {
            if (inputKitchenObjSO == recipeSO.input)
            {
                return recipeSO;
            }
        }
        return null;
    }
}
