using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _bar;


    private void OnEnable()
    {
        _health.ChaigeHealth += OnChaigeHealth;
    }

    private void OnDisable()
    {
        _health.ChaigeHealth -= OnChaigeHealth;
    }
    private void OnChaigeHealth(int maxHealth, int healt)
    {
        _bar.fillAmount = (float)healt / (float)maxHealth;
    }
}
