using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectPlayerCollider : MonoBehaviour
{
    public event UnityAction<Player> DetectPlayer;
    public event UnityAction NoPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            DetectPlayer?.Invoke(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            NoPlayer?.Invoke();
        }
    }
}
