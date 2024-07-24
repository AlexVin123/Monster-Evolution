using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class CameraControll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button _bgBt;
    [SerializeField] private CinemachineFreeLook _cam;
    private float _Yvalue;
    private float _Xvalue;

    private bool isDesktop;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDesktop == false)
        {
            _cam.m_XAxis.m_MaxSpeed = _Xvalue;
            _cam.m_YAxis.m_MaxSpeed = _Yvalue;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDesktop == false)
        {
            _cam.m_XAxis.m_MaxSpeed = 0;
            _cam.m_YAxis.m_MaxSpeed = 0;
        }
    }

    private void Start()
    {
        isDesktop = YandexGame.EnvironmentData.isDesktop;
        _Yvalue = _cam.m_YAxis.m_MaxSpeed;
        _Xvalue = _cam.m_XAxis.m_MaxSpeed;

        if (isDesktop == false)
        {
        _cam.m_XAxis.m_MaxSpeed = 0;
        _cam.m_YAxis.m_MaxSpeed = 0;
        }
    }

}
