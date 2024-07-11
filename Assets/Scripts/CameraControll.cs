using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _freeLookCam;
    private float _Yvalue;
    private float _Xvalue;

    private void Start()
    {
        _Yvalue = _freeLookCam.m_YAxis.Value;
        _Xvalue = _freeLookCam.m_XAxis.Value;
    }

    public void FrezeCam()
    {
        _freeLookCam.m_YAxis.Value = 0;
        _freeLookCam.m_XAxis.Value = 0;
    }

    public void UnFrezeCam()
    {
        _freeLookCam.m_YAxis.Value = _Yvalue;
        _freeLookCam.m_XAxis.Value = _Xvalue;
    }
}
