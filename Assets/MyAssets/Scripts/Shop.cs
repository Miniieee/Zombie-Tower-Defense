using UnityEngine;
using UnityEngine.UI;
using Unity.Burst;

[BurstCompile]
public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public GameObject shopCanvas;

    public Animator shopCanvasAnim;
    private string shopAnimBool = "NeedShop";

    public TurretBlueprints[] turets;
    public Button[] buttons;

    private void Start()
    {
        InvokeRepeating("CheckMoney", 0f, 1f);
        buildManager = BuildManager.instance;
    }

    private void CheckMoney()
    {
        int currentMoney = PlayerStats.Money;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (turets[i].cost > currentMoney)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void SelectTuret(int index)
    {
        shopCanvasAnim.SetBool(shopAnimBool, false);
        buildManager.SelectTurretToBuild(turets[index]);
    }
}
