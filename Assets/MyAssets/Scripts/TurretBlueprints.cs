using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;

[BurstCompile]
[System.Serializable]
public class TurretBlueprints
{
    //public int turetIndex;
    public string name;
    public GameObject prefab;
    public int cost;
    public int ammoCost;

    /*public GameObject upgradePrefab1;
    public int upgradeCost1;

    public GameObject upgradePrefab2;
    public int upgradeCost2;

    public GameObject upgradePrefab3;
    public int upgradeCost3;

    public GameObject upgradePrefab4;
    public int upgradeCost4;*/

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
