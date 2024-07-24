using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TouchCtrl : MonoBehaviour
{
    [SerializeField] float TouchSensitivity_x = 10f, TouchSensitivity_y = 10f;
    [SerializeField] float xmin, xmax, ymin, ymax;

    int InsideAreaTouchId = -1;
    bool Released = false;
    Touch AnalogTouch;
    Vector2 NormalizedAxis = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    bool CheckArea(Vector2 pos)
    {
        Vector2 npos = new Vector2(pos.x / Screen.width, pos.y / Screen.height);
        if (npos.x > xmin && npos.x < xmax && npos.y > ymin && npos.y < ymax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Released)
            {
                InsideAreaTouchId = GetAnalogTouchIDInsideArea(); //-1 = none
            }

            if (InsideAreaTouchId != -1)
            {
                AnalogTouch = Input.GetTouch(InsideAreaTouchId);
                if (Released)
                {
                    if (AnalogTouch.phase == TouchPhase.Began)
                    {
                        Released = false;
                        TouchBegan();
                    }
                }
                else
                {
                    if (AnalogTouch.phase == TouchPhase.Ended) TouchEnd();
                }
            }
            else
            {
                Released = true;
            }
        }
        else
        {
            InsideAreaTouchId = -1;
            Released = true;
        }
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {
            case "Mouse X":
                if (Input.touchCount > 0 && InsideAreaTouchId != -1)
                {
                    return Input.touches[InsideAreaTouchId].deltaPosition.x / TouchSensitivity_x;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }
            case "Mouse Y":
                if (Input.touchCount > 0 && InsideAreaTouchId != -1)
                {
                    return Input.touches[InsideAreaTouchId].deltaPosition.y / TouchSensitivity_y;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }
            default:
                Debug.LogError("Input <" + axisName + "> not recognized.", this);
                break;
        }
        return 0f;
    }

    int GetAnalogTouchIDInsideArea()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (CheckArea(Input.GetTouch(i).position))
                return i;
        }
        return -1;
    }

    void TouchBegan()
    {
        Released = false;
    }

    void TouchEnd()
    {
        Released = true;
        InsideAreaTouchId = -1;
    }
}