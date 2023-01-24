using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera Camera;
    protected Plane Plane;

    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;

        if (Input.touchCount >= 1)
        {
            Vector3 pos = Camera.transform.position;
            Delta1 = PlanePositionDelta(Input.GetTouch(0));

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                pos += Delta1/3;

                pos.x = Mathf.Clamp(pos.x, -20f, 15f);
                pos.z = Mathf.Clamp(pos.z, -13f, 20f);

                Camera.transform.position = pos;
            }
        }

    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

}
