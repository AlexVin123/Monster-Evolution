using JetBrains.Annotations;
using Supercyan.FreeSample;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 3.0f;
    [SerializeField] private SimpleSampleCharacterControl _characterControl;
    private float _rotationY = 0;
    private float _rotationX = 0;

    private Vector3 _startRotate;
    private Vector3 _currentRotation;
    private Vector3 _currentGlobalRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    public Vector3 CurrentGlobalRotation => _currentGlobalRotation;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    private void Start()
    {
        _startRotate = transform.rotation.eulerAngles;
    }

    void FixedUpdate()
    {
        if (_characterControl.CurrentVelocity > 0.2 || _characterControl.CurrentVelocity < -0.2)
        {
            Vector3 nextRotation = _startRotate;
            _currentGlobalRotation = transform.localRotation.eulerAngles;
            _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
            transform.localEulerAngles = _currentRotation;

        }
        else
        {
            int button = 0;
            _currentGlobalRotation = transform.localRotation.eulerAngles;

            if ((Input.GetMouseButton(button)))
            {
                float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
                //float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

                _rotationY += mouseX;
                //_rotationX += mouseY;

                _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

                Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

                _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
                transform.localEulerAngles = _currentRotation;
            }
        } 
    }

    public void ResetGlobalEuler()
    {
       transform.localEulerAngles = _startRotate;
    }
    
       
}
