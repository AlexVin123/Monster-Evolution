using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class DetectHealthCollider : MonoBehaviour
{
    public event UnityAction<Health> DetectHealth;
    public event UnityAction NoHealth;
    [SerializeField] private Health _health;

    public Health Health => _health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            _health = health;
            DetectHealth?.Invoke(health);
            _health.HealthEnd += OnPlayerDie;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            NoHealth?.Invoke();
            if (_health != null)
                _health.HealthEnd -= OnPlayerDie;
            _health = null;
        }
    }

    public void OnPlayerDie()
    {
        NoHealth?.Invoke();
        if(_health != null)
        _health.HealthEnd -= OnPlayerDie;
        _health = null;

    }
}
