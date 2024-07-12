using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : State
{
    [SerializeField] private AIMovement _movement;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _radius;

    private NavMeshPath _path;

    private Coroutine _procces;

    private void OnEnable()
    {
        _path = new NavMeshPath();
        _procces = StartCoroutine(Process());
    }

    private void OnDisable()
    {
        StopCoroutine(_procces);
        _procces = null;
    }

    private IEnumerator Process()
    {
        while (true) 
        {
            Vector3 point = GetRandomPoint();
            _movement.Move(point);
            yield return new WaitUntil(() => _movement.Completed == true);
        }
    }
    private Vector3 GetRandomPoint()
    {
        Vector3 randomPoin = Vector3.zero;
        bool correctPoin = false;
        while(correctPoin == false)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * _radius + transform.position, out navMeshHit, _radius, NavMesh.AllAreas);
            randomPoin = navMeshHit.position;

            _agent.CalculatePath(randomPoin, _path);
            if(_path.status == NavMeshPathStatus.PathComplete)
            {
                correctPoin = true;
            }

        }

        return randomPoin;
    }

}

