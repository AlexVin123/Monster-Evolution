using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Enemy
{

    [SerializeField] private State _firstState;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private List<PlayerView> _playerViews;
    [SerializeField] private List<string> _names;
    [SerializeField] private Health _health;

    private AttackCollider _attackCollider;

    private State _currentState;

    public AttackCollider AttackCollider => _attackCollider;
    public event Action<AICharacter> Died;

    public void Init(int lvl)
    {
        _attackCollider = _playerViews[lvl -1 ].AttackCollider;
        _attackCollider.Init(_playerViews[lvl - 1].PlayerData.Damage);
        _movement.OnModificationUpdate(_playerViews[lvl - 1].PlayerData.Speed);
        _health.Init(_playerViews[lvl - 1].PlayerData.Health, _playerViews[lvl - 1].PlayerData.Health);

        _health.HealthEnd += Die;

        ChaigeLvl(lvl);

        if (_currentState == null)
            _currentState = _firstState;

        _currentState.Enter();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _health.HealthEnd -= Die;
    }

    private void Update()
    {

        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    public void Run()
    {
        Transit(_firstState);
    }

    public void Warp(Vector3 position)
    {
        _movement.Warp(position);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }

    public void ChaigeLvl(int lvl)
    {

        foreach (PlayerView view in _playerViews)
        {
            view.gameObject.SetActive(false);
        }

        foreach (PlayerView view in _playerViews)
        {

            if (view.PlayerData.Lvl == lvl)
            {
                view.gameObject.SetActive(true);
                //_animator = view.Animator;
            }
        }
    }

    public void Die()
    {
        Died?.Invoke(this);
        _currentState = null;
        gameObject.SetActive(false);
    }
}
