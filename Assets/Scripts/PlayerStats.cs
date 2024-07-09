using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using YaAssets;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Exp _exp;
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _lvl;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _expBar;
    [SerializeField] private Image _healthBar;
    [SerializeField] private List<PlayerData> _player;


    public void init()
    {

        _exp.ChaigeExp += OnChaigeExp;
        _exp.ChaigeLvl += OnChaigeLvl;
        _health.ChaigeHealth += OnChaigeHealt;
    }

    public void PastInit(int lvl)
    {
        OnChaigeLvl(lvl);
    }

    private void OnEnable()
    {


    }

    private void OnDisable()
    {
        _exp.ChaigeExp -= OnChaigeExp;
        _exp.ChaigeLvl -= OnChaigeLvl;
        _health.ChaigeHealth -= OnChaigeHealt;
    }

    private void OnChaigeHealt(int maxHealth, int health)
    {
        _healthBar.fillAmount = (float)health / (float)maxHealth;
    }

    private void OnChaigeExp(int maxExp, int exp)
    {
        _expBar.fillAmount = (float)exp / (float)maxExp;
    }

    private void OnChaigeLvl(int lvl)
    {
        foreach (var player in _player)
        {
            if (player.Lvl == lvl)
            {
                _lvl.text = Localization.GetText("Level") + player.Lvl;
                _name.text = player.Name;
            }
        }
    }
}
