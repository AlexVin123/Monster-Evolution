using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private LoseWindow _loseWindow;
    [SerializeField] private SettingWindow _settingWindow;
    [SerializeField] private BosterWindow _bosterWindow;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TimerBoster _timerBoster;
    [SerializeField] private Exp _expPlayer;
    [SerializeField] private List<Boster> _bosters;

    public void Awake()
    {
        Cursor.visible = false;
        _playerStats.init();
        _player.Init();
        _playerStats.PastInit(_player.Lvl);
        _player.Die += _loseWindow.Open;
        _loseWindow.Revival += _player.OnRevival;
        _loseWindow.RewardRevival += _player.OnRewardRevival;
        _spawner.StartSpawn();
        _bosterWindow.TakedReward += _expPlayer.OnExpBoosterClicked;
        _bosterWindow.TakedReward += OnBoostTaked;
        _expPlayer.BosterEnd += OnBoostEnd;
        _expPlayer.BosterTaked += _timerBoster.StartTimer;
        BoosterAdd();
   
    }

    private void OnDisable()
    {
        _player.Die -= _loseWindow.Open;
        _loseWindow.Revival -= _player.OnRevival;
        _loseWindow.RewardRevival -= _player.OnRewardRevival;
        _bosterWindow.TakedReward -= _expPlayer.OnExpBoosterClicked;
        _bosterWindow.TakedReward -= OnBoostTaked;
        _expPlayer.BosterEnd -= OnBoostEnd;
        _expPlayer.BosterTaked -= _timerBoster.StartTimer;
        BoosterRemove();
    }

    private void BoosterAdd()
    {
        foreach (var item in _bosters) 
        {
            item.PlayerOnTriggerEnter += _bosterWindow.Open;
        }
    }

    private void BoosterRemove()
    {
        foreach (var item in _bosters)
        {
            item.PlayerOnTriggerEnter -= _bosterWindow.Open;
        }
    }

    private void OnBoostTaked()
    {
        foreach (var b in _bosters)
        {
            b.gameObject.SetActive(false);
        }
    }

    private void OnBoostEnd()
    {
        foreach (var b in _bosters) 
        {
            b.gameObject.SetActive(true);
        }
    }
}
