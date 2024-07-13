using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDistansAttackTransit : Transition
{
    [SerializeField] private DetectHealthCollider _detectHealthCollider;

    private void Update()
    {

        if (_detectHealthCollider.Health == null)
            return;



       bool needTransit = false;

        if (Vector3.Distance(transform.position, _detectHealthCollider.Health.transform.position) > 1f)
            needTransit = true;

        NeedTransit = needTransit;
    }
}
