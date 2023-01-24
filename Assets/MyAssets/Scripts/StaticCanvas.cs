using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst;

[BurstCompile]
public class StaticCanvas : MonoBehaviour
{
    [SerializeField] GameObject shopCanvas;
    [SerializeField] Animator shopCanvasAnim;
    private string shopAnimBool = "NeedShop";

    public void ShowShopCanvas()
    {
        shopCanvasAnim.SetBool(shopAnimBool, true);
    }

    public void HideShopCanvas()
    {
        shopCanvasAnim.SetBool(shopAnimBool, false);
    }
}
