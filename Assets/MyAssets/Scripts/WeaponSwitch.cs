using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Burst;

[BurstCompile]
public class WeaponSwitch : MonoBehaviour
{
    private int selectedWeapon;
    [Header("Reload Buttons")]
    public GameObject pistolReload;
    public GameObject AKReload;

    [Header("Buttons")]
    public Button pistolButton;
    public Button AK47Button;

    [Header("Colors")]
    public Color selectedColor;
    public Color unselectedColor;


    public void PistolButton()
    {
        //set button colors
        pistolButton.image.color = selectedColor;
        AK47Button.image.color = unselectedColor;


        selectedWeapon = 0;
        PlayerPrefs.SetInt("WeaponButton", 0);
        pistolReload.SetActive(true);
        AKReload.SetActive(false);
        SelectWeapon();
    }

    public void AKButton()
    {
        //set button colors
        pistolButton.image.color = unselectedColor;
        AK47Button.image.color = selectedColor;

        selectedWeapon = 1;
        PlayerPrefs.SetInt("WeaponButton", 1);
        pistolReload.SetActive(false);
        AKReload.SetActive(true);
        SelectWeapon();
    }

    public void Start()
    {
        InitialiseAtStart();
        SelectWeapon();
    }

    private void InitialiseAtStart()
    {
        //button color initialise
        pistolButton.image.color = selectedColor;
        AK47Button.image.color = unselectedColor;

        selectedWeapon = 0;
        PlayerPrefs.SetInt("WeaponButton", 0);
        pistolReload.SetActive(true);
        AKReload.SetActive(false);
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.GetComponent<Weapon>().AmmoCheck();
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.GetComponent<Weapon>().AmmoCheck();
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
