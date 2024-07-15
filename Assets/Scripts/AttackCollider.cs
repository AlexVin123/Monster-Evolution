using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private Exp _exp;
    [SerializeField] private Animator _attackTrAnim;
    bool _attaced = false;
    private IEnemy _enemy;

    private int _damage;

    public void Init(int damage)
    {
        _damage = damage;
    }

    public void OnEnable()
    {
        if (_attackTrAnim != null)
            _attackTrAnim.SetTrigger("Damage");
    }

    public void Init(int damage, Exp exp)
    {
        _exp = exp;
        _damage = damage;
    }

    private void OnDisable()
    {
        _enemy = null;
        _attaced = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Health health) && _attaced == false)
        {
            bool EnemyFind = false;

            if (other.gameObject.TryGetComponent(out IEnemy enemy))
            {
                _enemy = enemy;
                health.HealthEnd += OnEnemyDie;
                EnemyFind = true;
            }
            if (health.NoHealth == false)
                health.RemoveHealth(_damage);

            if (EnemyFind == true)
                health.HealthEnd -= OnEnemyDie;

            _attaced = true;
        }
    }

    public void OnEnemyDie()
    {
        _exp.AddExp(_enemy.Exp);
    }
}
