using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : Enemy
{
    [SerializeField] private DetectHealthCollider _detecter;
    [SerializeField] private AttackCollider _attacker;
    [SerializeField] private float _delayAttack = 2;

    private Health _healthEnemy;
    private Coroutine _attack;
    private WaitForSeconds _delay;

    protected override void Awake()
    {
        base.Awake();
        _delay = new WaitForSeconds(_delayAttack);
        _attacker.gameObject.SetActive(false);
        _attacker.Init(_enemyData.Damage);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _detecter.DetectPlayer += OnPlayerDetect;
        _detecter.NoPlayer += OnNoPlayer;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _detecter.DetectPlayer -= OnPlayerDetect;
        _detecter.NoPlayer -= OnNoPlayer;
    }

    private void Update()
    {
        if (_healthEnemy != null)
        {
            Vector3 playerPos = new Vector3(_healthEnemy.transform.position.x, 0, _healthEnemy.transform.position.z);
            transform.LookAt(playerPos);
        }
    }

    protected override void OnNoHealth()
    {
        base.OnNoHealth();
        _detecter.OnPlayerDie();
        if (_attack != null)
            StopCoroutine(_attack);
        _attack = null;
        _attacker.gameObject.SetActive(false);
    }

    private void OnPlayerDetect(Health health)
    {
        _healthEnemy = health;
        animator.SetBool("Attack", true);
        _attack = StartCoroutine(AttackCorutine());
    }

    private void OnNoPlayer()
    {
        _healthEnemy = null;
        animator.SetBool("Attack", false);
        if(_attack != null)
        StopCoroutine(_attack);
        _attack = null;
        _attacker.gameObject.SetActive(false);
    }

    private IEnumerator AttackCorutine()
    {
        while (true)
        {
            yield return _delay;
            _attacker.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _attacker.gameObject.SetActive(false);
        }
    }
}
