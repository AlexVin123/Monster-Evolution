using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnemyState : State
{
    [SerializeField] private DetectHealthCollider _detectHealthCollider;
    [SerializeField] private AIMovement _move;

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
                _move.Move(_healthEnemy.transform.position);

                yield return new WaitUntil(() => _move.Completed == true);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
