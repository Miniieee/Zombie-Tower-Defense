using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst;

[BurstCompile]
public class CameraSwap : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject fpsCamera;
    public GameObject tdCamera;
    
    [Header("Buttons")]
    public GameObject FPSButton;
    public GameObject TDButton;
    public GameObject ShopButton;
    public GameObject Player;

    [Header("Canvases")]
    public GameObject FPSCanvas;
    public GameObject nodeCanvas;
    public GameObject nodeUI;
    [SerializeField] Animator shopCanvasAnim;
    public GameObject shopCanvas;
    private string shopAnimBool = "NeedShop";

    [HideInInspector] public BoxCollider[] nodes;

    private void Start()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        nodes = new BoxCollider[bases.Length];
        for (int i = 0; i < bases.Length; ++i)
            nodes[i] = bases[i].GetComponent<BoxCollider>();

        tdCamera.SetActive(true);
        fpsCamera.SetActive(false);
        shopCanvas.SetActive(true);

        FPSButton.SetActive(true);
        ShopButton.SetActive(true);
        FPSCanvas.SetActive(false);
        nodeCanvas.SetActive(true);
        TDButton.SetActive(false);

        Player.GetComponent<View>().enabled = false;
    }


    public void SetFPSCamera()
    {
        foreach (BoxCollider node in nodes)
        {
            node.enabled = false;
        }

        shopCanvasAnim.SetBool(shopAnimBool, false);

        fpsCamera.SetActive(true);
        tdCamera.SetActive(false);

        FPSButton.SetActive(false);
        TDButton.SetActive(true);
        ShopButton.SetActive(false);

        FPSCanvas.SetActive(true);
        nodeCanvas.SetActive(false);

        shopCanvas.SetActive(false);

        Player.GetComponent<View>().enabled = true;
    }

    public void SetTDCamera()
    {
        foreach (BoxCollider node in nodes)
        {
            node.enabled = true;
        }

        nodeUI.GetComponentInChildren<Node>().enabled = false;

        fpsCamera.SetActive(false);
        tdCamera.SetActive(true);

        FPSButton.SetActive(true);
        ShopButton.SetActive(true);
        TDButton.SetActive(false);

        FPSCanvas.SetActive(false);
        nodeCanvas.SetActive(true);
        shopCanvas.SetActive(true);

        Player.GetComponent<View>().enabled = false;
    }
}
