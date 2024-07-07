using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerStats _playerStats;

    public void Awake()
    {
        _playerStats.init();
        _player.Init();
        _playerStats.PastInit(_player.Lvl);
    }
}
