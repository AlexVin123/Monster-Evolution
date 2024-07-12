using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{
    private AttackCollider _attackCollider;
    [SerializeField] private float _timeAttack = 0.1f;
    private Animator _animator;
    private Coroutine _attackCorutine;
    private WaitForSeconds _delay;


    public void Init(Animator animator, int damage, AttackCollider attackCollider, Exp exp)
    {
        _delay = new WaitForSeconds(_timeAttack);
        _attackCollider = attackCollider;
        _attackCollider.Init(damage,exp);
        _animator = animator;
        _attackCollider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(_attackCorutine == null)
            {
                _animator.SetTrigger("Attack");
                _attackCorutine = StartCoroutine(TakeDamage());
            }
        }
    }

    private IEnumerator TakeDamage()
    {
        _attackCollider.gameObject.SetActive(true);
        yield return _delay;
        _attackCollider.gameObject.SetActive(false);
        _attackCorutine = null;

    }
}
