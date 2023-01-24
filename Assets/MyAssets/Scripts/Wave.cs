using UnityEngine;
using Unity.Burst;

[BurstCompile]
[System.Serializable]
public class Wave
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public int enemyCount;

    public float minTimeBetweenWaves;
    public float maxTimeBetweenWaves;
}
