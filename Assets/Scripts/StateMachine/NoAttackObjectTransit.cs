using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAttackObjectTransit : Transition
{
    [SerializeField] private DetectHealthCollider _detectHealthCollider;

    private void Update()
    {
        bool needTransit = false;

        if(_detectHealthCollider.Health == null)
            needTransit = true;

        NeedTransit = needTransit;

    }
}
