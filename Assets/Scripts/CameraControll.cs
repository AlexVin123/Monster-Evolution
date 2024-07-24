using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraControll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button _bgBt;
    [SerializeField] private CinemachineFreeLook _cam;
    private float _Yvalue;
    private float _Xvalue;

    public void OnPointerDown(PointerEventData eventData)
    {
        _cam.m_XAxis.m_MaxSpeed = _Xvalue;
        _cam.m_YAxis.m_MaxSpeed = _Yvalue;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        _cam.m_XAxis.m_MaxSpeed = 0;
        _cam.m_YAxis.m_MaxSpeed = 0;
    }

    private void Start()
    {
        _Yvalue = _cam.m_YAxis.m_MaxSpeed;
        _Xvalue = _cam.m_XAxis.m_MaxSpeed;
        _cam.m_XAxis.m_MaxSpeed = 0;
        _cam.m_YAxis.m_MaxSpeed = 0;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            _cam.m_XAxis.m_MaxSpeed = _Yvalue;
            _cam.m_YAxis.m_MaxSpeed = _Xvalue;
        }

        if(Input.GetMouseButtonUp(1)) 
        {

            _cam.m_XAxis.m_MaxSpeed = 0;
            _cam.m_YAxis.m_MaxSpeed = 0;
        }
    }

}
