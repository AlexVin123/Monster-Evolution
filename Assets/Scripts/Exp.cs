using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Events;

public class Exp : MonoBehaviour
{
    private int _currentLvl;
    private int _currentExp = 0;
    private int _maxExp;
    private int _expForNextLvl;
    private List<PlayerView> _players;

    public event UnityAction<int> ChaigeLvl;
    public event UnityAction<int, int> ChaigeExp;

    public int CurrentLvl => _currentLvl;

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
            Debug.LogError("Получаемый опыт меньше 0");

        _currentExp += exp;

        if (_currentExp >= _expForNextLvl)
        {
            NextLvl();
        }

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

}
