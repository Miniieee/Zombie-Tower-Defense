using UnityEngine;
using Unity.Burst;

[BurstCompile]
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 500;

    public static int Lives;
    public int startLives;

    public static int Rounds;
    public int rounds = 0;

    void Awake()
    {
        startLives = PlayerPrefs.GetInt("Health", 15);
        startMoney = PlayerPrefs.GetInt("CollectedMoney", 500);
        Money = startMoney;
        Lives = startLives;
        Rounds = rounds;
    }
}
