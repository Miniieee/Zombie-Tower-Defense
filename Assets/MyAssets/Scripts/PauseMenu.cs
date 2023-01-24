using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Button reaction assigns")]
    public GameObject pauseUI;

    public void ResumeButton()
    {
        Time.timeScale = 1;

        pauseUI.SetActive(false);
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("GemsPerGame", 0);
        Time.timeScale = 1;
        WaveSpawner.EnemiesAlive = 0;
        PlayerPrefs.SetInt("GemsPerGame", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseUI.SetActive(false);
    }

    public void Menu()
    {
        PlayerPrefs.SetInt("GemsPerGame", 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
