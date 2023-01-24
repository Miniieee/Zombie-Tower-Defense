using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Burst;

[BurstCompile]
public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool buttonPressed = false;
    public static ShootButton instace;
    bool isPressed = false;

    private void Start()
    {
        if (instace != null)
        {
            Debug.LogError("More than one instace");
            return;
        }

        instace = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        print("not pressed");
        buttonPressed = false;
    }
}
