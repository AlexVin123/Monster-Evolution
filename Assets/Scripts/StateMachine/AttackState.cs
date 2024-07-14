using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private DetectHealthCollider _detectHealthCollider;
    [SerializeField] private AICharacter _chatacter;
    [SerializeField] private AIMovement _move;
    [SerializeField] private GameObject _ai;

    //private Health _healthEnemy;
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
        _move.Stop();

        while (true)
        {

            yield return new WaitForSeconds(0.5f);
            Vector3 playerPos = new Vector3(_detectHealthCollider.Health.transform.position.x, 0, _detectHealthCollider.Health.transform.position.z);
            _ai.transform.LookAt(playerPos);
            _chatacter.AttackCollider.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _chatacter.AttackCollider.gameObject.SetActive(false);
        }
    }
}
