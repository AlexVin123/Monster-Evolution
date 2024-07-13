using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesAttackObjectTransition : Transition
{
    [SerializeField] private DetectHealthCollider _detectHealthCollider;

    private void Update()
    {
        bool needTransit = true;

        if (_detectHealthCollider.Health == null)
        {
   
            needTransit = false;
        }

        if (_detectHealthCollider.Health != null)
        {
            if (Vector3.Distance(transform.position, _detectHealthCollider.Health.transform.position) > 1f)
            {
                needTransit = false;
            }
        }

        NeedTransit = needTransit;

    }
}
