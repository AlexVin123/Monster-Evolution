using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] private GameObject _visual;
    [SerializeField] private Collider _collider;
    [SerializeField] protected Animator animator;
    [SerializeField] private float _timeDie;

    private Health _health;
    private WaitForSeconds _regenerate;
    private WaitForSeconds _delayDie;
    public int Exp => _enemyData.Exp;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
        _delayDie = new WaitForSeconds(_timeDie);
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

    protected virtual void OnNoHealth()
    {
        StartCoroutine(DieCorutine());
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        yield return _regenerate;
        if (animator != null)
            animator.SetBool("Die", false);
        _visual.SetActive(true);
        _collider.enabled = true;
        _health.Init(_enemyData.Health, _enemyData.Health);
    }

    private IEnumerator DieCorutine()
    {
        if (animator != null)
            animator.SetBool("Die", true);
        _collider.enabled = false;

        yield return _delayDie;

        _visual.SetActive(false);
    }
}
