using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class DetectPlayerCollider : MonoBehaviour
{
    public event UnityAction<Player> DetectPlayer;
    public event UnityAction NoPlayer;
    private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _player = player;
            DetectPlayer?.Invoke(player);
            _player.Die += OnPlayerDie;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            NoPlayer?.Invoke();
            _player.Die -= OnPlayerDie;
        }
    }

    public void OnPlayerDie()
    {
        NoPlayer?.Invoke();
        _player.Die -= OnPlayerDie;
    }
}
