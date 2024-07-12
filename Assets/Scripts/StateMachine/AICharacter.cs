using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : MonoBehaviour
{

    [SerializeField] private State _firstState;
    [SerializeField] private AIMovement _movement;

    private State _currentState;


    public void Init()
    {

    }

    private void Update()
    {

        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    public void Run()
    {
        Transit(_firstState);
    }

    public void Warp(Vector3 position)
    {
        _movement.Warp(position);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }
}
