using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


using Unity.Burst;

[BurstCompile]
public class ScenesToManage : MonoBehaviour
{
    [Header("LoadingScene")]
    public GameObject loadingScene;
    public GameObject upgradeSelect;
    public GameObject levelSelector;
    public Slider slider;
    public TextMeshProUGUI percentageText;

    [Header("Upgrade Avialalbe Sign")]
    public GameObject upgradeAvialable;

    [Header("Animations")]
    public Animator optionAnim;
    public GameObject optionsToManage;
    public Animator rotateOptionAnim;

    [Header("Texts To Write Out")]
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI cdCostText;
    public TextMeshProUGUI hCostText;
    public TextMeshProUGUI dmCostText;


    // private variables
    private int gems;
    private int cdCost;
    private int hCost;
    private int dmCost;



    void Start()
    {
        InvokeRepeating("WriteOutEverything", 0f, 0.5f);
    }

    public void WriteOutEverything()
    {
        cdCost = PlayerPrefs.GetInt("cdCost", 10);
        hCost = PlayerPrefs.GetInt("hCost", 10);
        dmCost = PlayerPrefs.GetInt("dmCost", 10);

        gems = PlayerPrefs.GetInt("Gems", 30);

        gemsText.text = gems.ToString();
        cdCostText.text = cdCost.ToString();
        hCostText.text = hCost.ToString();
        dmCostText.text = dmCost.ToString();

        if (gems >= cdCost || gems >= hCost || gems >= dmCost)
        {
            upgradeAvialable.SetActive(true);
        }
        else
        {
            upgradeAvialable.SetActive(false);
        }
    }

    public void StartButton(int sceneIndex)
    {
        upgradeSelect.SetActive(false);
        levelSelector.SetActive(false);
        StartCoroutine(SceneLoader(sceneIndex));
    }

    IEnumerator SceneLoader(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScene.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            float progressPercentage = progress * 100f;
            percentageText.text = progressPercentage.ToString("F0") + "%";

            yield return null;
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void UpgradeButton()
    {
        upgradeSelect.SetActive(!upgradeSelect.activeInHierarchy);
    }

    public void SettingsButton()
    {
        optionsToManage.SetActive(!optionsToManage.activeInHierarchy);
        optionAnim.SetBool("IsIn",optionsToManage.activeInHierarchy);
        rotateOptionAnim.SetBool("rotateClockwise", optionsToManage.activeInHierarchy);
    }

    public void StartButtonClick()
    {
        levelSelector.SetActive(!levelSelector.activeInHierarchy);
    }
}
