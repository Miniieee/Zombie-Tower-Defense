using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.Burst;

[BurstCompile]
public class UpgradesMenu : MonoBehaviour
{
    public Button cooldownButton;
    public Button healthButton;
    public Button doubleButton;

    public TextMeshProUGUI cdText;
    public TextMeshProUGUI hText;
    public TextMeshProUGUI dmText;


    private string coolDownState = "CoolDownState";
    private string healthState = "HealthState";
    private string doubleMoneyState = "doubleMoneyState";

    private string coolDown = "CoolDown";
    private string health = "Health";
    private string doubleMoney = "doubleMoney";

    private int cdState;
    private int hState;
    private int dmState;

    private int gems;

    private int cdCost;
    private int hCost;
    private int dmCost;

    void Start()
    {
        //DebuggerMethod();
        ConstantCheck();
        InvokeRepeating("ConstantCheck", 0f, 1f);
    }

    public void DebuggerMethod()
    {
        PlayerPrefs.SetInt("Gems", 80);
        PlayerPrefs.SetInt(coolDownState, 0);
        PlayerPrefs.SetInt(healthState, 4);
        PlayerPrefs.SetInt(doubleMoneyState, 0);

        PlayerPrefs.SetInt("cdCost", 10);
        PlayerPrefs.SetInt("hCost", 10);
        PlayerPrefs.SetInt("dmCost", 10);
        PlayerPrefs.SetInt("LevelReached", 1);
    }

    private void ConstantCheck()
    {
        cdCost = PlayerPrefs.GetInt("cdCost", 10);
        hCost = PlayerPrefs.GetInt("hCost", 10);
        dmCost = PlayerPrefs.GetInt("dmCost", 10);

        gems = PlayerPrefs.GetInt("Gems", 30);

        cdState = PlayerPrefs.GetInt(coolDownState, 0);
        hState = PlayerPrefs.GetInt(healthState, 0);
        dmState = PlayerPrefs.GetInt(doubleMoneyState, 0);

        InteractableCheck();
    }

    private void InteractableCheck()
    {
        if (cdCost > gems)
        {
            cooldownButton.interactable = false;
        }
        else
        {
            cooldownButton.interactable = true;
        }

        if (hCost > gems)
        {
            healthButton.interactable = false;
        }
        else
        {
            healthButton.interactable = true;
        }

        if (dmCost > gems)
        {
            doubleButton.interactable = false;
        }
        else
        {
            doubleButton.interactable = true;
        }
    }

    public void CoolDown()
    {
        if (cdState == 0 && gems >= cdCost)
        {
            //gems decrase
            gems -= cdCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetFloat(coolDown, 45);

            //cdState upgrade
            cdState = 1;
            PlayerPrefs.SetInt(coolDownState, cdState);
            //cdCost incrase
            cdCost = 30;
            cdText.text = cdCost.ToString();
            PlayerPrefs.SetInt("cdCost", cdCost);
        }
        else if (cdState == 1 && gems >= cdCost)
        {
            //gems decrase
            gems -= cdCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetFloat(coolDown, 60);

            //cdState upgrade
            cdState = 2;
            PlayerPrefs.SetInt(coolDownState, cdState);
            //cdCost incrase
            cdCost = 50;
            cdText.text = cdCost.ToString();
            PlayerPrefs.SetInt("cdCost", cdCost);
        }
        else if (cdState == 2 && gems >= cdCost)
        {
            //gems decrase
            gems -= cdCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetFloat(coolDown, 100);

            //cdState upgrade
            cdState = 3;
            PlayerPrefs.SetInt(coolDownState, cdState);
            //cdCost incrase
            cdCost = 100;
            cdText.text = cdCost.ToString();
            PlayerPrefs.SetInt("cdCost", cdCost);
        }
        else if (cdState == 3 && gems >= cdCost)
        {
            //gems decrase
            gems -= cdCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetFloat(coolDown, 120);

            //cdState upgrade
            cdState = 4;
            PlayerPrefs.SetInt(coolDownState, cdState);
            //cdCost incrase
            cdCost = 200;
            cdText.text = cdCost.ToString();
            PlayerPrefs.SetInt("cdCost", cdCost);
        }
        else if (cdState == 4 && gems >= cdCost)
        {
            //gems decrase
            gems -= cdCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetFloat(coolDown, 150);

            //cdState upgrade
            cdState = 5;
            PlayerPrefs.SetInt(coolDownState, cdState);
            //cdCost incrase
            cdCost = 500;
            cdText.text = cdCost.ToString();
            PlayerPrefs.SetInt("cdCost", cdCost);
        }
        else if (cdState == 5 && gems >= cdCost)
        {
            //gems decrase
            gems -= cdCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetFloat(coolDown, 200);

            //cdState upgrade
            cdState = 6;
            cdText.text = "MAX";
            PlayerPrefs.SetInt(coolDownState, 6);
        }
        if (cdState == 6)
        {
            cdText.text = "MAX";
        }
        InteractableCheck();
    }

    public void BaseHealth()
    {
        if (hState == 0 && gems >= hCost)
        {
            //gems decrase
            gems -= hCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(health, 15);

            //cdState upgrade
            hState = 1;
            PlayerPrefs.SetInt(healthState, hState);
            //cdCost incrase
            hCost = 30;
            hText.text = hCost.ToString();
            PlayerPrefs.SetInt("hCost", hCost);
        }
        else if (hState == 1 && gems >= hCost)
        {
            gems -= hCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(health, 18);

            //cdState upgrade
            hState = 2;
            PlayerPrefs.SetInt(healthState, hState);
            //cdCost incrase
            hCost = 50;
            hText.text = hCost.ToString();
            PlayerPrefs.SetInt("hCost", hCost);
        }
        else if (hState == 2 && gems >= hCost)
        {
            gems -= hCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(health, 20);

            //cdState upgrade
            hState = 3;
            PlayerPrefs.SetInt(healthState, hState);
            //cdCost incrase
            hCost = 100;
            hText.text = hCost.ToString();
            PlayerPrefs.SetInt("hCost", hCost);
        }
        else if (hState == 3 && gems >= hCost)
        {
            gems -= hCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(health, 22);

            //cdState upgrade
            hState = 4;
            PlayerPrefs.SetInt(healthState, hState);
            //cdCost incrase
            hCost = 200;
            hText.text = hCost.ToString();
            PlayerPrefs.SetInt("hCost", hCost);
        }
        else if (hState == 4 && gems >= hCost)
        {
            gems -= hCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(health, 25);

            //cdState upgrade
            hState = 5;
            PlayerPrefs.SetInt(healthState, hState);
            //cdCost incrase
            hCost = 500;
            hText.text = hCost.ToString();
            PlayerPrefs.SetInt("hCost", hCost);
        }
        else if (hState == 5 && gems >= hCost)
        {
            gems -= hCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(health, 30);

            //cdState upgrade
            hState = 6;
            hText.text = "MAX";
            PlayerPrefs.SetInt(healthState, hState);
        }
        if (hState == 6)
        {
            hText.text = "MAX";
        }
        InteractableCheck();
    }

    public void DoubleMoney()
    {
        if (dmState == 0 && gems >= dmCost)
        {
            //gems decrase
            gems -= dmCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(doubleMoney, 15);

            //cdState upgrade
            dmState = 1;
            PlayerPrefs.SetInt(doubleMoneyState, dmState);
            //cdCost incrase
            dmCost = 30;
            dmText.text = dmCost.ToString();
            PlayerPrefs.SetInt("dmCost", dmCost);
        }
        else if (dmState == 1 && gems >= dmCost)
        {
            //gems decrase
            gems -= dmCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(doubleMoney, 20);

            //cdState upgrade
            dmState = 2;
            PlayerPrefs.SetInt(doubleMoneyState, dmState);
            //cdCost incrase
            dmCost = 50;
            dmText.text = dmCost.ToString();
            PlayerPrefs.SetInt("dmCost", dmCost);
        }
        else if (dmState == 2 && gems >= dmCost)
        {
            //gems decrase
            gems -= dmCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(doubleMoney, 25);

            //cdState upgrade
            dmState = 3;
            PlayerPrefs.SetInt(doubleMoneyState, dmState);
            //cdCost incrase
            dmCost = 100;
            dmText.text = dmCost.ToString();
            PlayerPrefs.SetInt("dmCost", dmCost);
        }
        else if (dmState == 3 && gems >= dmCost)
        {
            //gems decrase
            gems -= dmCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(doubleMoney, 30);

            //cdState upgrade
            dmState = 4;
            PlayerPrefs.SetInt(doubleMoneyState, dmState);
            //cdCost incrase
            dmCost = 200;
            dmText.text = dmCost.ToString();
            PlayerPrefs.SetInt("dmCost", dmCost);
        }
        else if (dmState == 4 && gems >= dmCost)
        {
            //gems decrase
            gems -= dmCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(doubleMoney, 40);

            //cdState upgrade
            dmState = 5;
            PlayerPrefs.SetInt(doubleMoneyState, dmState);
            //cdCost incrase
            dmCost = 500;
            dmText.text = dmCost.ToString();
            PlayerPrefs.SetInt("dmCost", dmCost);
        }
        else if (dmState == 5 && gems >= dmCost)
        {
            //gems decrase
            gems -= dmCost;
            PlayerPrefs.SetInt("Gems", gems);

            //cooldown time incrase
            PlayerPrefs.SetInt(doubleMoney, 60);

            //cdState upgrade
            dmState = 6;
            dmText.text = "MAX";
            PlayerPrefs.SetInt(doubleMoneyState, dmState);
        }
        if (dmState == 6)
        {
            dmText.text = "MAX";
        }
        InteractableCheck();
    }
}
