using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [field:SerializeField] public string Name { get;private set; }
    [field: SerializeField] public int Damage {  get; private set; }
    [field: SerializeField] public int Exp { get; private set; }
    [field: SerializeField] public float TimeRegenerate { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
}
