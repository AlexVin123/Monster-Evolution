using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _bar;
    [SerializeField] private TextMeshProUGUI _text;


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
        _text.text = healt + "/" + maxHealth;
        _bar.value = (float)healt / (float)maxHealth;
    }
}
