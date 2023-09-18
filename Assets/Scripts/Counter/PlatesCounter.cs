using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjSO platesSO;
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;
    private float spawnPlateTime;
    [SerializeField] private float spawnPlateTimeMax = 4f;
    private int countPlateOnCounter;
    [SerializeField] private int maxPlateOnCounter = 4;
    private void Update()
    {
        if(spawnPlateTime > spawnPlateTimeMax)
        {
            if (countPlateOnCounter < maxPlateOnCounter) {
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                countPlateOnCounter++;
                spawnPlateTime = 0;                
            }
        }
        else
        {
            spawnPlateTime += Time.deltaTime;
        }
    }
    public override void Interact(PlayerController player)
    {
        if(!player.HasKitchenObj() && countPlateOnCounter > 0)
        {
            countPlateOnCounter--;
            spawnPlateTime = 0;
            OnPlateRemove?.Invoke(this, EventArgs.Empty);
            KitchenObj.SpawnKitchenObj(platesSO, player);
        }
    }
}
