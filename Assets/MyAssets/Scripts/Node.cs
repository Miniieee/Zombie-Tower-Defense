using UnityEngine;
using Unity.Burst;

[BurstCompile]
public class Node : MonoBehaviour
{
    public Color hoverColor;

    [HideInInspector]
    public GameObject turret;
    public GameObject emptyTurret;
    public Turet ammoAmount;
    public ParticleSystem buildParticle;
    [HideInInspector]
    public TurretBlueprints turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    public int upgradeIndex = 0;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (!buildManager.HasMoney)
        {
            return;
        }

        BuildTurret(buildManager.TurretToBuild());
    }

    public void BuildTurret(TurretBlueprints blueprint)
    {
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
        turret = _turret;

        if (turret != null)
        {
            emptyTurret.SetActive(false);
            buildParticle.Play();
        }

        turretBlueprint = blueprint;
        ammoAmount = turret.GetComponent<Turet>();
        if (ammoAmount == null)
        {
            ammoAmount = turret.GetComponentInChildren<Turet>();
        }
        upgradeIndex = 0;
    }

/*
    public void UpgradeTurret1()
    {
        PlayerStats.Money -= turretBlueprint.upgradeCost1;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab1, transform.position, Quaternion.identity);
        turret = _turret;

        upgradeIndex = 1;
    }
    public void UpgradeTurret2()
    {
        PlayerStats.Money -= turretBlueprint.upgradeCost2;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab2, transform.position, Quaternion.identity);
        turret = _turret;

        upgradeIndex = 2;
    }
    public void UpgradeTurret3()
    {
        PlayerStats.Money -= turretBlueprint.upgradeCost3;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab3, transform.position, Quaternion.identity);
        turret = _turret;

        upgradeIndex = 3;
    }
    public void UpgradeTurret4()
    {
        PlayerStats.Money -= turretBlueprint.upgradeCost4;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab4, transform.position, Quaternion.identity);
        turret = _turret;

        upgradeIndex = 4;

        isUpgraded = true;
    }

    */
    public void SellTurret()
    {
        upgradeIndex = 0;
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
        emptyTurret.SetActive(true);
    }

    public void Ammo()
    {
        ammoAmount.MaxAmmo();
        ammoAmount.AmmoStatus();
    }
}
