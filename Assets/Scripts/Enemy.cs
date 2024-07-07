using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] private GameObject _visual;
    [SerializeField] private Collider _collider;
    [SerializeField] protected Animator animator;

    private Health _health;
    WaitForSeconds _regenerate;
    public int Exp => _enemyData.Exp;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
        _regenerate = new WaitForSeconds(_enemyData.TimeRegenerate);
        _health.Init(_enemyData.Health, _enemyData.Health);
    }

    protected virtual void OnEnable()
    {
        _health.NoHealth += OnNoHealth;
    }

    protected virtual void OnDisable()
    {
        _health.NoHealth -= OnNoHealth;
    }

    private void OnNoHealth()
    {
        _visual.SetActive(false);
        _collider.enabled = false;
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        yield return _regenerate;
        _visual.SetActive(true);
        _collider.enabled = true;
        _health.Init(_enemyData.Health, _enemyData.Health);
    }
}
