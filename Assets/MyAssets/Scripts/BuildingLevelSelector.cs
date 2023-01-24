using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevelSelector : MonoBehaviour
{
    private int selectedLevel;

    public void Start()
    {
        selectedLevel = PlayerPrefs.GetInt("HealthState", 0);
        SelectLevel();
    }

    public void SelectLevel()
    {
        int i = 0;
        foreach (Transform level in transform)
        {
            if (i == selectedLevel)
            {
                level.gameObject.SetActive(true);
            }
            else
            {
                level.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
