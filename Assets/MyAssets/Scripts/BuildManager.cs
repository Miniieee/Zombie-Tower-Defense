using UnityEngine;
using Unity.Burst;

[BurstCompile]
public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [HideInInspector] public TurretBlueprints turretToBuild;

    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;

    private Node selectedNode;
    public NodeUI nodeUI;

    public int upgradeIndex = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one Buildmanagger");
            return;
        }

        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }


    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        upgradeIndex = 0;
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild (TurretBlueprints turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprints TurretToBuild()
    {
        return turretToBuild;
    }
}
