using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private LoseWindow _loseWindow;

    public void Awake()
    {
        _playerStats.init();
        _player.Init();
        _playerStats.PastInit(_player.Lvl);
        _player.Die += _loseWindow.Open;
        _loseWindow.Revival += _player.OnRevival;
        _loseWindow.RewardRevival += _player.OnRewardRevival;
    }

    private void OnDisable()
    {
        _player.Die -= _loseWindow.Open;
        _loseWindow.Revival -= _player.OnRevival;
        _loseWindow.RewardRevival -= _player.OnRewardRevival;
    }
}
