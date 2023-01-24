using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.Burst;

[BurstCompile]
public class GameOver : MonoBehaviour
{
    [Header("Write outs")]
    public int coins;
    public TextMeshProUGUI collectedGems;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI collectedMoneyText;
    public Animator shopCanvasAnim;

    private int multiplierCoins;
    private int actualCoins;
    private int getCoins;

    void Start()
    {
        actualCoins = PlayerPrefs.GetInt("Gems",0);
        multiplierCoins = Random.Range(2, 5);

        coins = PlayerPrefs.GetInt("GemsPerGame", 0);
        collectedGems.text = coins.ToString();
        actualCoins += coins;

        getCoins = coins * multiplierCoins - coins;

        multiplierText.text = "For extra " + getCoins.ToString() + " coins";
        PlayerPrefs.SetInt("Gems", actualCoins);
        collectedMoneyText.text = "$" + PlayerPrefs.GetInt("CollectedMoney", 500);
        shopCanvasAnim.SetBool("NeedShop", false);
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("GemsPerGame", 0);
        Time.timeScale = 1;
        WaveSpawner.EnemiesAlive = 0;
        PlayerPrefs.SetInt("GemsPerGame", 0);
        PlayerStats.Rounds = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        PlayerStats.Rounds = 0;
        PlayerPrefs.SetInt("GemsPerGame", 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ExtraCoins()
    {
        coins = coins + getCoins;
        actualCoins += getCoins;
        PlayerPrefs.SetInt("Gems", actualCoins);
        collectedGems.text = coins.ToString();
    }

    public void NextLevelLoad()
    {
        PlayerStats.Rounds = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
