using UnityEngine;
using UnityEngine.UI;
using Unity.Burst;

[BurstCompile   ]
public class DropTouch : MonoBehaviour
{
    public WeaponSwitch weapon;
    public DoubleMoney doubleMoney;
    public Weapon pistol;
    public Weapon ak47;
    [HideInInspector] public int countdownTime;
    [HideInInspector] public bool isAvialable = false;
    [HideInInspector] public int gemsPerGame;

    private void Start()
    {
        gemsPerGame = PlayerPrefs.GetInt("GemsPerGame", 0);
        countdownTime = PlayerPrefs.GetInt("Countdown",30);
    }

    private void Update()
    {
        if ((Input.touchCount > 0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.transform.tag == "Gun")
                {
                    int ammo = Random.Range(5, 15);

                    pistol.AddAmmo(ammo);
                    DropMethod(raycastHit);
                }

                if (raycastHit.transform.tag == "AK47")
                {
                    int ammo = Random.Range(10, 20);

                    ak47.AddAmmo(ammo);
                    DropMethod(raycastHit);
                }

                if (raycastHit.transform.tag == "2x")
                {
                    doubleMoney.SetCoolDown();
                    Destroy(raycastHit);
                }

                if (raycastHit.transform.tag == "Coin")
                {
                    gemsPerGame++;
                    PlayerPrefs.SetInt("GemsPerGame", gemsPerGame);
                    Destroy(raycastHit);
                }
            }
        }
    }

    private void DropMethod(RaycastHit raycasthit)
    {
        isAvialable = true;

        Destroy(raycasthit);
    }

    private static void Destroy(RaycastHit raycasthit)
    {
        Destroy(raycasthit.transform.gameObject);
    }
}
