using UnityEngine;
using TMPro;

public class DoubleMoney : MonoBehaviour
{
    private float coolDown;
    private bool isActivated;
    public TextMeshProUGUI countDown;
    public GameObject coundownText;

    void Start()
    {
        coundownText.SetActive(false);
        isActivated = false;
    }

    void Update()
    {

        if (coolDown > 0f)
        {
            coolDown -= Time.deltaTime;
            countDown.text = "2X money " + coolDown.ToString("0") + " sec";
            if (!isActivated)
            {
                PlayerPrefs.SetInt("Double", 2);
                coundownText.SetActive(true);
                isActivated = true;
            }
        }
        else
        {
            if (isActivated)
            {
                PlayerPrefs.SetInt("Double", 1);
                coundownText.SetActive(false);
                isActivated = false;
            }
        }
    }

    public void SetCoolDown()
    {
        coolDown = PlayerPrefs.GetFloat("doubleMoney", 30f);
    }
}
