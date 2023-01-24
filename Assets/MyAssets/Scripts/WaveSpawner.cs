using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Burst;

[BurstCompile]
public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    private bool isWaveStarted = false;

    public Transform enemyPrefab;
    public static int spawnIndex;

    private float timeBetweenWaves;
    private int enemyToSpawn;
    private GameObject spawnEnemyPrefab;
    public GameObject gameEnded;

    public Transform[] spawnpoints;

    [Header("Wave properties")]
    public Wave[] waves;
    private int waveIndex = 0;

    private void Awake()
    {
        isWaveStarted = false;
        EnemiesAlive = 0;
    }

    void Update()
    {
        if (EnemiesAlive == 0 && !isWaveStarted)
        {
            StartCoroutine(Waves());
            return;
        }
        else
        {
            return;
        }
    }

    IEnumerator Waves()
    {
        isWaveStarted = true;

        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.enemyCount;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            timeBetweenWaves = Random.Range(wave.minTimeBetweenWaves, wave.maxTimeBetweenWaves);

            PickEnemyToSpawn(wave);

            SpawnEnemy(spawnEnemyPrefab);

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        isWaveStarted = false;
        waveIndex++;            

        if (waveIndex == waves.Length)
        {
            StartNextScene();
            this.enabled = false;
        }

    }

    public void StartNextScene()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        levelReached = SceneManager.GetActiveScene().buildIndex + 1;
        print(levelReached);
        print("player pref: " + PlayerPrefs.GetInt("LevelReached", 1));
        Time.timeScale = 0;
        gameEnded.SetActive(true);
        if (PlayerPrefs.GetInt("LevelReached", 1) < levelReached)
        {
            PlayerPrefs.SetInt("LevelReached", levelReached);
        }
    }

    private void PickEnemyToSpawn(Wave wave)
    {
        enemyToSpawn = Random.Range(0, 100);

        if (enemyToSpawn <= 25)
        {
            spawnEnemyPrefab = wave.enemy1;
        }
        else if (enemyToSpawn > 25 && enemyToSpawn <= 50)
        {
            spawnEnemyPrefab = wave.enemy2;
        }
        else if (enemyToSpawn > 50 && enemyToSpawn <= 70)
        {
            spawnEnemyPrefab = wave.enemy3;
        }
        else if (enemyToSpawn > 70 && enemyToSpawn <= 90)
        {
            spawnEnemyPrefab = wave.enemy4;
        }
        else
        {
            spawnEnemyPrefab = wave.enemy5;
        }
    }


    void SpawnEnemy(GameObject enemy)
    {
        spawnIndex = Random.Range(0, spawnpoints.Length);

        Instantiate(enemy, spawnpoints[spawnIndex].transform.position, Quaternion.identity);
    }

    public void RestartCounter()
    {
        waveIndex = 0;
    }
}
