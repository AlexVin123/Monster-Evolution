using Supercyan.FreeSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<PlayerView> _playerViews;
    [SerializeField] private Animator _animator;

    private Attack _attack;
    private Health _health;
    private SimpleSampleCharacterControl _characterControl;
    private Exp _exp;

    public int Lvl => _exp.CurrentLvl;

    public void Init()
    {
        _health = GetComponent<Health>();
        _exp = GetComponent<Exp>();
        _attack = GetComponent<Attack>();
        _exp.Init(_playerViews);
        _attack.Init(_animator,DamageToLvl(_exp.CurrentLvl));
        OnChaigeLvl(_exp.CurrentLvl);
        InitHealth();
        _characterControl = GetComponent<SimpleSampleCharacterControl>();
        _characterControl.Init(_animator);
        _exp.ChaigeLvl += OnChaigeLvl;

    }

    private void OnDisable()
    {
        _exp.ChaigeLvl -= OnChaigeLvl;
    }

    private void InitHealth()
    {
        int CurrentLvl = _exp.CurrentLvl;
        int maxHealth = 0;

        foreach (PlayerView playerView in _playerViews)
        {
            if (playerView.PlayerData.Lvl == CurrentLvl)
            {
                maxHealth = playerView.PlayerData.Health;
            }
        }

        _health.Init(maxHealth, maxHealth);
    }

    public void OnChaigeLvl(int lvl)
    {
        bool activeView = false;

        foreach (PlayerView view2 in _playerViews)
        {
            view2.gameObject.SetActive(false);
        }

        foreach (PlayerView view in _playerViews)
        {

            if (view.PlayerData.Lvl == lvl)
            {
                view.gameObject.SetActive(true);
                _animator = view.Animator;
                activeView = true;
            }
        }

        if (activeView == false)
        {
            _playerViews[_playerViews.Count - 1].gameObject.SetActive(true);
            _animator = _playerViews[_playerViews.Count - 1].Animator;
        }

        _attack.Init(_animator, DamageToLvl(_exp.CurrentLvl));

    }

    private int DamageToLvl(int lvl)
    {
        foreach (PlayerView view in _playerViews) 
        {
            if (view.PlayerData.Lvl == lvl)
                return view.PlayerData.Damage;
        }

        return _playerViews[_playerViews.Count - 1].PlayerData.Damage;
    }
}
