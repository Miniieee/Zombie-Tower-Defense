using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Burst;

[BurstCompile]
public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject ui;
    public Camera TDCamera;

    public TextMeshProUGUI sellCost;
    public Button ammoButton;
    public TextMeshProUGUI ammoCost;

    private int costAmmo;

    private float fixedScale = 0.0015f;

    private void Ammobuttoncheck()
    {
        Scale();

        costAmmo = target.turretBlueprint.ammoCost;
        int currentMoney = PlayerStats.Money;

        if (costAmmo > currentMoney)
        {
            ammoButton.interactable = false;
        }
        else
        {
            ammoButton.interactable = true;
        }
    }

    private void Scale()
    {
        var distance = (TDCamera.transform.position - transform.position).magnitude;
        var size = distance * fixedScale * TDCamera.fieldOfView;
        transform.localScale = Vector3.one * size;
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.transform.position + new Vector3(0f,1.9f,0f);

        /*if(upgradeIndex == 0)
        {
            upgradeButton.interactable = true;
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost1;
        }
        if (upgradeIndex == 1)
        {
            upgradeButton.interactable = true;
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost2;
        }
        if (upgradeIndex == 2)
        {
            upgradeButton.interactable = true;
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost3;
        }
        if (upgradeIndex == 3)
        {
            upgradeButton.interactable = true;
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost4;
        }

        if (target.isUpgraded)
        {
            upgradeCost.text = "Upgraded";
            upgradeButton.interactable = false;
        }*/

        sellCost.text = "$" + target.turretBlueprint.GetSellAmount().ToString();
        ammoCost.text = "$" + target.turretBlueprint.ammoCost.ToString();

        ui.SetActive(true);
        Ammobuttoncheck();
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

  /*  public void Upgrade()
    {
        if (upgradeIndex == 0)
        {
            if (PlayerStats.Money < target.turretBlueprint.upgradeCost1)
            {
                print("no money");
                return;
            }
            else
            {
                target.UpgradeTurret1();
                upgradeCost.text = "$" + target.turretBlueprint.upgradeCost2;
                sellCost.text = "$" + (target.turretBlueprint.cost / 2).ToString();
            }
        }

        if (upgradeIndex == 1)
        {
            if (PlayerStats.Money < target.turretBlueprint.upgradeCost2)
            {
                print("no money");
                return;
            }
            else
            {
                target.UpgradeTurret2();
                upgradeCost.text = "$" + target.turretBlueprint.upgradeCost3;
                sellCost.text = "$" + (target.turretBlueprint.cost / 2).ToString();
            }
        }

        if (upgradeIndex == 2)
        {
            if (PlayerStats.Money < target.turretBlueprint.upgradeCost3)
            {
                print("no money");
                return;
            }
            else
            {
                target.UpgradeTurret3();
                upgradeCost.text = "$" + target.turretBlueprint.upgradeCost4;
                sellCost.text = "$" + (target.turretBlueprint.cost / 2).ToString();
            }
        }

        if (upgradeIndex == 3)
        {
            if (PlayerStats.Money < target.turretBlueprint.upgradeCost4)
            {
                print("no money");
                return;
            }
            else
            {
                target.UpgradeTurret4();
                upgradeCost.text = "$" + target.turretBlueprint.upgradeCost4;
                sellCost.text = "$" + (target.turretBlueprint.cost / 2).ToString();
            }
        }

        BuildManager.instance.DeselectNode();
    }*/

    public void Sell()
    {
        PlayerStats.Money += target.turretBlueprint.cost / 2;

        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Ammo()
    {
        costAmmo = target.turretBlueprint.ammoCost;
        int currentMoney = PlayerStats.Money;

        if (costAmmo <= currentMoney)
        {
            PlayerStats.Money -= target.turretBlueprint.ammoCost;
            target.Ammo();
        }
        Ammobuttoncheck();
        BuildManager.instance.DeselectNode();
    }
}
