using Supercyan.FreeSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private List<PlayerView> _playerViews;
    [SerializeField] private Animator _rootAnimator;
    [SerializeField] private ParticleSystem _upLvlFx;
    private Animator _animator;
    [SerializeField] private float _timeDie;
    [SerializeField][Range(0, 1)] private float _modificatorRemoveExp = 0.15f;

    private Attack _attack;
    private Health _health;
    private SimpleSampleCharacterControl _characterControl;
    private Exp _exp;
    private WaitForSeconds _dieTimer;
    private Vector3 _startPoint;

    public int Lvl => _exp.CurrentLvl;
    public bool dying { get; private set; }

    public event UnityAction Die;

    public void Init()
    {
        _startPoint = transform.position;
        _dieTimer = new WaitForSeconds(_timeDie);
        _health = GetComponent<Health>();
        _exp = GetComponent<Exp>();
        _attack = GetComponent<Attack>();
        _characterControl = GetComponent<SimpleSampleCharacterControl>();
        _exp.Init(_playerViews);
        _attack.Init(_animator, DamageToLvl(_exp.CurrentLvl), AttackColliderToLvl(_exp.CurrentLvl), _exp);
        OnChaigeLvl(_exp.CurrentLvl);
        InitHealth();
        _characterControl.Init(_animator);
        _exp.ChaigeLvl += OnChaigeLvl;
        _health.HealthEnd += OnDie;

    }

    private void OnDisable()
    {
        _exp.ChaigeLvl -= OnChaigeLvl;
        _health.HealthEnd -= OnDie;
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
        if (maxHealth == 0)
            maxHealth = _playerViews[_playerViews.Count - 1].PlayerData.Health;
        _health.Init(maxHealth, maxHealth);
    }

    public void OnChaigeLvl(int lvl)
    {
        bool activeView = false;

        if (_rootAnimator != null)
            _rootAnimator.SetTrigger("UpLvl");

        if (_upLvlFx != null)
            _upLvlFx.Play();

        foreach (PlayerView view in _playerViews)
        {
            view.gameObject.SetActive(false);
        }

        foreach (PlayerView view in _playerViews)
        {

            if (view.PlayerData.Lvl == lvl)
            {
                view.gameObject.SetActive(true);
                _animator = view.Animator;
                _characterControl.Init(_animator);
                activeView = true;
            }
        }

       



        if (activeView == false)
        {
            _playerViews[_playerViews.Count - 1].gameObject.SetActive(true);
            _animator = _playerViews[_playerViews.Count - 1].Animator;
            _characterControl.Init(_animator);
        }
        _attack.Init(_animator, DamageToLvl(_exp.CurrentLvl), AttackColliderToLvl(_exp.CurrentLvl), _exp);
        _characterControl.Upgrade(SpeedToLvl(_exp.CurrentLvl), ForceJumpToLvl(_exp.CurrentLvl));

    }

    private float ForceJumpToLvl(int lvl)
    {
        foreach (PlayerView view in _playerViews)
        {
            if (view.PlayerData.Lvl == lvl)
                return view.PlayerData.ForgeJump;
        }

        return _playerViews[_playerViews.Count - 1].PlayerData.ForgeJump;
    }

    private float SpeedToLvl(int lvl)
    {
        foreach (PlayerView view in _playerViews)
        {
            if (view.PlayerData.Lvl == lvl)
                return view.PlayerData.Speed;
        }

        return _playerViews[_playerViews.Count - 1].PlayerData.Speed;
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

    private AttackCollider AttackColliderToLvl(int lvl)
    {
        foreach (PlayerView view in _playerViews)
        {
            if (view.PlayerData.Lvl == lvl)
                return view.AttackCollider;
        }

        return _playerViews[_playerViews.Count - 1].AttackCollider;
    }

    private void OnDie()
    {
        StartCoroutine(DieCorutine());
    }

    private IEnumerator DieCorutine()
    {
        _animator.SetBool("Die", true);
        _characterControl.enabled = false;
        dying = true;
        yield return _dieTimer;
        Die?.Invoke();
    }

    public void OnRevival()
    {
        InitHealth();
        _animator.SetBool("Die", false);
        int removeExp = (int)(_exp.CurrentExp * _modificatorRemoveExp);
        Debug.Log("RemoveExp " + removeExp);
        _exp.RemoveExp(removeExp);
        transform.position = _startPoint;
        dying = false;
        _characterControl.enabled = true;
    }

    public void OnRewardRevival()
    {
        InitHealth();
        _animator.SetBool("Die", false);
        transform.position = _startPoint;
        _characterControl.enabled = true;
    }

    public int ShowRemoveExp()
    {
        Debug.Log("CurrentExp:" + _exp.CurrentExp + "Modificator:" + _modificatorRemoveExp + "Value:" + (int)(_exp.CurrentExp * _modificatorRemoveExp));
        return (int)(_exp.CurrentExp * _modificatorRemoveExp);
    }
}
