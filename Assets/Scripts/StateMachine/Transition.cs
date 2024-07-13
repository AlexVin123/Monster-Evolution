using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;

    [field: SerializeField]public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
        Enable();
    }

    protected virtual void Enable() { }
}
