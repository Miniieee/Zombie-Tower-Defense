using UnityEngine;
using Unity.Burst;

[BurstCompile]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool GameHasEnded = false;
    public AudioManager audioManager;

    [Header("UI Links")]
    public GameObject gameOverUI;
    private static int adIndex = 0;

    [Header("Advertisements")]
    public UnityAdManager adManager;

    [Header("Button reaction assigns")]
    public GameObject pauseUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one gamemanager");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = -1;
        Time.timeScale = 1;
        GameHasEnded = false;
        PlayerPrefs.SetInt("GemsPerGame", 0);
        audioManager.Play("Background");
        audioManager.Play("MoanStart");
    }

    public void EndGame()
    {
        GameHasEnded = true;
        gameOverUI.SetActive(true);
        adIndex = PlayerPrefs.GetInt("AdIndex", 0);
        adIndex++;
        PlayerPrefs.SetInt("AdIndex", adIndex);
        if (adIndex >= 3)
        {
            adManager.PlayInterstitialAd();
            adIndex = 0;
            PlayerPrefs.SetInt("AdIndex", adIndex);
        }
        Time.timeScale = 0;
    }

    public void PauseButton()
    {
        Time.timeScale = 0;

        pauseUI.SetActive(true);
    }
}
