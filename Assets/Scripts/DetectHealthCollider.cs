using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class DetectHealthCollider : MonoBehaviour
{
    public event UnityAction<Health> DetectPlayer;
    public event UnityAction NoPlayer;
    [SerializeField]private Health _health;

    public Health Health => _health;   

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health health))
        {
            _health = health;
            DetectPlayer?.Invoke(health);
            _health.HealthEnd += OnPlayerDie;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Health player))
        {
            NoPlayer?.Invoke();
            _health.HealthEnd -= OnPlayerDie;
            _health = null;
        }
    }

    public void OnPlayerDie()
    {
        NoPlayer?.Invoke();
        if(_health != null)
        _health.HealthEnd -= OnPlayerDie;

    }
}
