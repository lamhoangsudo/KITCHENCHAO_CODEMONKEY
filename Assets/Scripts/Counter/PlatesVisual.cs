using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesVisual : MonoBehaviour
{
    [SerializeField] private Transform spawnPostion;
    [SerializeField] private Transform platesVisual;
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private float locationSpawnY = .1f;
    private List<GameObject> listPlatesVisual;
    private void Start()
    {
        listPlatesVisual = new List<GameObject>();
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemove += PlatesCounter_OnPlateRemove;
    }

    private void PlatesCounter_OnPlateRemove(object sender, EventArgs e)
    {
        GameObject plates = listPlatesVisual[^1];
        Destroy(plates);
        listPlatesVisual.Remove(plates);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, EventArgs e)
    {
        Transform plateSpawn = Instantiate(platesVisual, spawnPostion);
        plateSpawn.localPosition = new Vector3(0, locationSpawnY * listPlatesVisual.Count, 0);
        listPlatesVisual.Add(plateSpawn.gameObject);
    }
}
