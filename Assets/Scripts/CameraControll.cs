using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _freeLookCam;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _smooth = 1;
    private int _mouse = 0;
    private float _Yvalue;
    private float _Xvalue;

    private void Start()
    {
        _Yvalue = _freeLookCam.m_YAxis.Value;
        _Xvalue = _freeLookCam.m_XAxis.Value;
    }
    private void Update()
    {
        //Debug.Log(_rb.velocity.magnitude);
        if(_rb.velocity.magnitude == 0)
        {
            if (Input.GetMouseButton(_mouse))
            {
                _freeLookCam.m_YAxis.m_MaxSpeed = 2;
                _freeLookCam.m_XAxis.m_MaxSpeed = 300;
            }
            else if (Input.GetMouseButtonUp(_mouse))
            {
                _freeLookCam.m_YAxis.m_MaxSpeed = 0;
                _freeLookCam.m_XAxis.m_MaxSpeed = 0;
            }

            if(_rb.velocity.magnitude < 4 && _rb.velocity.magnitude != 0)
            {
                _freeLookCam.m_XAxis.Value = Mathf.MoveTowards(_freeLookCam.m_XAxis.Value, _rb.transform.localRotation.eulerAngles.y, _smooth * Time.deltaTime);
                _freeLookCam.m_YAxis.Value = Mathf.MoveTowards(_freeLookCam.m_YAxis.Value, _Yvalue, _smooth * Time.deltaTime);
            }
        }
        else
        {
            //_freeLookCam.m_XAxis.Value = Mathf.MoveTowards(_freeLookCam.m_XAxis.Value, _rb.transform.localRotation.eulerAngles.y, _smooth * Time.deltaTime);
            //_freeLookCam.m_YAxis.Value = Mathf.MoveTowards(_freeLookCam.m_YAxis.Value, _Yvalue, _smooth * Time.deltaTime);
            //Debug.Log(_rb.transform.rotation.eulerAngles.y);
        }


    }
}
