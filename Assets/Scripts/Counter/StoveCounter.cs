using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProcess
{
    public event EventHandler<OnStateChangeEventArg> OnStateChange;
    public event EventHandler<IHasProcess.OnPrecessChangerEventArgs> OnProcessBar;
    public class OnStateChangeEventArg : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private KitchenRecipeFryingSO[] recipeFrying;
    private float timeFrying;
    private float timeFryed;
    private KitchenRecipeFryingSO fryingSO;
    private State state;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObj())
        {
            switch (state)
            {
                case State.Idle:
                    timeFrying = 0;
                    timeFryed = 0;
                    break;
                case State.Frying:
                    timeFrying += Time.deltaTime;
                    OnProcessBar?.Invoke(this, new IHasProcess.OnPrecessChangerEventArgs
                    {
                        processNormalized = timeFrying / fryingSO.Time
                    });
                    if (timeFrying > fryingSO.Time)
                    {
                        this.GetKitchenObj().DestroyKitchenObj();
                        KitchenObj.SpawnKitchenObj(fryingSO.meatOutput, this);
                        fryingSO = GetOutputKitchenRecipeFryingSO(GetKitchenObj().GetKitchenObjSO());
                        state = State.Fried;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
                    }
                    break;
                case State.Fried:
                    timeFryed += Time.deltaTime;
                    OnProcessBar?.Invoke(this, new IHasProcess.OnPrecessChangerEventArgs
                    {
                        processNormalized = timeFryed / fryingSO.Time
                    });
                    if (timeFryed > fryingSO.Time)
                    {
                        this.GetKitchenObj().DestroyKitchenObj();
                        KitchenObj.SpawnKitchenObj(fryingSO.meatOutput, this);
                        state = State.Burned;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
                    }
                    break;
                case State.Burned:
                    state = State.Idle;
                    OnProcessBar?.Invoke(this, new IHasProcess.OnPrecessChangerEventArgs
                    {
                        processNormalized = 0f
                    });
                    break;
            }

        }
    }
    public override void Interact(PlayerController player)
    {
        if (HasKitchenObj() == false)
        {
            if (player.HasKitchenObj())
            {
                //go to counter
                if (GetOutputKitchenRecipeFryingSO(player.GetKitchenObj().GetKitchenObjSO()) != null)
                {
                    player.GetKitchenObj().SetKitchenObjParent(this);
                    fryingSO = GetOutputKitchenRecipeFryingSO(this.GetKitchenObj().GetKitchenObjSO());
                    if (fryingSO.meatInput.objName.Contains("Uncooked"))
                    {
                        state = State.Frying;
                        timeFrying = 0;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
                    }
                    else if (fryingSO.meatInput.objName.Contains("Cook"))
                    {
                        state = State.Fried;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
                    }
                    else
                    {
                        state = State.Burned;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
                    }
                }
            }
            else
            {
                state = State.Idle;
                OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
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
                        if (state != State.Frying)
                        {
                            state = State.Idle;
                            timeFrying = 0;
                            OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
                        }                       
                        timeFryed = 0;
                        OnProcessBar?.Invoke(this, new IHasProcess.OnPrecessChangerEventArgs { processNormalized = 0f });
                    }
                }
            }
            else
            {
                if (state != State.Frying)
                {
                    GetKitchenObj().SetKitchenObjParent(player);
                    state = State.Idle;
                    timeFrying = 0;
                    OnStateChange?.Invoke(this, new OnStateChangeEventArg { state = this.state });
                }
                timeFryed = 0;
                OnProcessBar?.Invoke(this, new IHasProcess.OnPrecessChangerEventArgs { processNormalized = 0f });
            }
        }
    }
    private KitchenRecipeFryingSO GetOutputKitchenRecipeFryingSO(KitchenObjSO inputKitchenObjSO)
    {
        foreach (KitchenRecipeFryingSO recipeFryingSO in recipeFrying)
        {
            if (inputKitchenObjSO == recipeFryingSO.meatInput)
            {
                return recipeFryingSO;
            }
        }
        return null;
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
}
