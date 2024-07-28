using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private int _health;
    private int _maxHealth;

    public event UnityAction<int, int> ChaigeHealth;
    public event UnityAction HealthEnd;

    public bool NoHealth => _health <= 0;

    public int MaxHealth => _maxHealth;
    public void Init(int maxHealth, int health)
    {
        if (maxHealth > 0)
            _maxHealth = maxHealth;
        else
            Debug.LogError("Максимальное здоровье не может быть 0");

        if (health > 0)
            if (health > maxHealth)
            {
                _health = maxHealth;
                ChaigeHealth?.Invoke(_maxHealth, _health);
            }
            else
            { 
                _health = health;
                ChaigeHealth?.Invoke(_maxHealth, _health);
            }
        else
            Debug.LogError("Здоровье не может быть 0");

    }

    public void AddHealth(int health)
    {
        if(health < 0)
            Debug.LogError("Значение здоровья не может быть меньше 0");

        if (_health + health > _maxHealth)
        {
            _health = _maxHealth;
            ChaigeHealth?.Invoke(_maxHealth, _health);
        }
        else
        {
            _health += health;
            ChaigeHealth?.Invoke(_maxHealth, _health);
        }
    }

    public void RemoveHealth(int health)
    {
        if (health < 0)
            Debug.LogError("Значение здоровья не может быть меньше 0");

        if (_health - health <= 0)
        {
            _health = 0;
            ChaigeHealth?.Invoke(_maxHealth, _health);
            HealthEnd?.Invoke();
        }
        else
        {
            _health -= health;
            ChaigeHealth?.Invoke(_maxHealth, _health);
        }
    }
}
