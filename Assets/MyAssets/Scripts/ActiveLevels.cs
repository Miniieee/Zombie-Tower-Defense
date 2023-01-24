using UnityEngine;
using UnityEngine.UI;

public class ActiveLevels : MonoBehaviour
{
    public Button[] levels;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        for (int i = 0; i < levels.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levels[i].interactable = false;
            }
            else
            {
                levels[i].interactable = true;
            }
        }
    }
}
