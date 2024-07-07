using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerView : MonoBehaviour
{
    [field:SerializeField] public PlayerData PlayerData {  get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
}
