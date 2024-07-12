using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [field:SerializeField]public int Lvl {  get; private set; }
    [field: SerializeField] public int Exp { get; private set; }
    [field: SerializeField] public int Health {  get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float ForgeJump {  get; private set; }
}
