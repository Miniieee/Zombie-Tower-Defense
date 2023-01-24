using UnityEngine;
using TMPro;
using Unity.Burst;
using System.Collections;

[BurstCompile]
public class MoneyCounter : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI collectedMoneyText;

    public static int collectedMoney; 

    private void Start()
    {
        collectedMoney = PlayerPrefs.GetInt("CollectedMoney", 500);
        MoneyUpdate(0);
        InvokeRepeating("StatCheck", 0f, 0.1f);
    }

    public void MoneyUpdate(int value)
    {
        collectedMoney += value;
        PlayerPrefs.SetInt("CollectedMoney", collectedMoney);
        StatCheck();
    }

    public void StatCheck()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
        healthText.text = PlayerStats.Lives.ToString();
        waveText.text = PlayerStats.Rounds.ToString() + " / 5";
    }
}
