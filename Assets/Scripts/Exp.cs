using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using YG;
using Button = UnityEngine.UI.Button;

public class Exp : MonoBehaviour
{
    [SerializeField] private Button _expBooster;
    [SerializeField] private float _boostDuration = 15f;
    private bool _isBoostActive = false;
    private int _expRate;
    private int _originalExpRate = 1;
    private int _boostedExpRate = 2;
    
    private int _currentLvl;
    private int _currentExp = 0;
    private int _maxExp;
    private int _expForNextLvl;
    private List<PlayerView> _players;

    public event UnityAction<int> ChaigeLvl;
    public event UnityAction<int, int> ChaigeExp;

    public int CurrentLvl => _currentLvl;
    public int CurrentExp => _currentExp;
    
    private void Start()
    {
        _expBooster.onClick.AddListener(OnExpBoosterClicked);
        _expRate = _originalExpRate;
    }

    public void Init(List<PlayerView> players)
    {
        _players = players;
        _currentLvl = 1;

        if (PlayerPrefs.HasKey("Lvl"))
            _currentLvl = PlayerPrefs.GetInt("Lvl");

        _maxExp = LvlToMaxExp(_currentLvl);
        _expForNextLvl = LvlToMaxExp(_currentExp + 1);

        if (PlayerPrefs.HasKey("Exp"))
            _currentExp = PlayerPrefs.GetInt("Exp");

        ChaigeExp?.Invoke(_maxExp, _currentExp);
    }

    public void AddExp(int exp)
    {
        if (exp < 0)
            Debug.LogError("Количество опыта не может быть меньше 0");

        _currentExp += exp * _expRate;

        if (_currentExp >= _expForNextLvl)
        {
            NextLvl();
        }

        PlayerPrefs.SetInt("Exp", _currentExp);
        ChaigeExp?.Invoke(_maxExp, _currentExp);
    }

    public void RemoveExp(int exp)
    {
        if (exp < 0)
            Debug.LogError("Количество опыта не может быть меньше 0");

        _currentExp -= exp;
        PlayerPrefs.SetInt("Exp", _currentExp);
        ChaigeExp?.Invoke(_maxExp, _currentExp);
    }

    public void NextLvl()
    {
        _currentLvl++;
        _maxExp = LvlToMaxExp(_currentLvl);
        _expForNextLvl = LvlToMaxExp(_currentLvl + 1);
        PlayerPrefs.SetInt("Lvl", _currentLvl);
        ChaigeLvl.Invoke(_currentLvl);
    }

    public int LvlToMaxExp(int lvl)
    {
        foreach (PlayerView player in _players)
        {
            if (player.PlayerData.Lvl == _currentLvl)
                return player.PlayerData.Exp;
        }

        return (int)((float)_maxExp + ((float)_maxExp * 0.25));
    }
    
    private void OnExpBoosterClicked()
    {
        YandexGame.RewVideoShow(onReward: OnAdvertisingOnComplete);
    }

    private void OnAdvertisingOnComplete()
    {
        StartCoroutine(StartExpBoost());
    }

    private IEnumerator StartExpBoost()
    {
        _isBoostActive = true;
        _expBooster.interactable = false;
        _expRate = _boostedExpRate;
        
        Debug.Log("Ваш буст составляет: " + _expRate);
        
        yield return new WaitForSeconds(_boostDuration);

        _isBoostActive = false;
        _expBooster.interactable = true;
        
        _expRate = _originalExpRate;
        
        Debug.Log("Ваш буст вернулся в норму: " + _expRate);
    }

}
