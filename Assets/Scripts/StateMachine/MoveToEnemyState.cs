using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToEnemyState : State
{
    [SerializeField] private DetectHealthCollider _detectHealthCollider;
    [SerializeField] private AIMovement _move;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AICharacter _character;

    private Health _healthEnemy;
    private Coroutine _procces;

    private void OnEnable()
    {
        _procces = StartCoroutine(Procces());
    }

    private void OnDisable()
    {
        StopCoroutine(_procces);
    }

    private IEnumerator Procces()
    {
        while (true)
        {
            _healthEnemy = _detectHealthCollider.Health;

            if (_healthEnemy != null)
            {

                //_move.Stop();
                _character.Animator.SetFloat("MoveSpeed", _agent.speed);
                _move.Move(_healthEnemy.transform.position);

                yield return new WaitUntil(() => _move.Completed == true);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
