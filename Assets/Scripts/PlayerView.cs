using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerView : MonoBehaviour
{
    [field:SerializeField] public PlayerData PlayerData {  get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public AttackCollider AttackCollider { get; private set; }

    public void Init(int damage, Exp exp)
    {
        AttackCollider.Init(damage, exp);
    }
}
