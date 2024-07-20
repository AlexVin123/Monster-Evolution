using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boster : MonoBehaviour
{
    public event Action PlayerOnTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            PlayerOnTriggerEnter?.Invoke();
        }
    }
}
