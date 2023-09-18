using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class KitchenObjSO : ScriptableObject
{
    public Transform prefab;
    [SerializeField] public Sprite sprite;
    public string objName;
}

