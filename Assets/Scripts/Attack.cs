using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;
using YG;

public class Attack : MonoBehaviour
{
    private AttackCollider _attackCollider;
    [SerializeField] private float _timeAttack = 0.1f;
    private Animator _animator;
    private Coroutine _attackCoroutine;
    private WaitForSeconds _delay;

    [SerializeField] private Button _buttonAttack;

    private bool _isDesktop;

    private void Awake()
    {
        _isDesktop = YandexGame.EnvironmentData.isDesktop;

        if (_isDesktop == false)
        {
            if (_buttonAttack != null)
            {
                _buttonAttack.onClick.AddListener(OnAttackButtonPressed);
            }
            else
            {
                _buttonAttack.gameObject.SetActive(false);
            }
        }
        else
        {
            _buttonAttack.gameObject.SetActive(false);
        }

    }

    public void Init(Animator animator, int damage, AttackCollider attackCollider, Exp exp)
    {
        _delay = new WaitForSeconds(_timeAttack);
        _attackCollider = attackCollider;
        _attackCollider.Init(damage, exp);
        _animator = animator;
        _attackCollider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _isDesktop)
        {
            // if(_attackCorutine == null)
            // {
            //     _animator.SetTrigger("Attack");
            //     _attackCorutine = StartCoroutine(TakeDamage());
            // }

            TriggerAttack();
        }
    }

    private void OnAttackButtonPressed()
    {
        TriggerAttack();
    }

    private void TriggerAttack()
    {
        if (_attackCoroutine == null)
        {
            if (_animator != null)
                _animator.SetTrigger("Attack");
            if (_attackCollider != null)
                _attackCoroutine = StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        _attackCollider.gameObject.SetActive(true);
        yield return _delay;
        _attackCollider.gameObject.SetActive(false);
        _attackCoroutine = null;
    }
}
