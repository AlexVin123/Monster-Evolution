using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoseWindow : Window
{
    [SerializeField] private GameObject _root;
    [SerializeField] private Button _buttonRevival;
    [SerializeField] private Button _buttonRewardRevival;

    public event UnityAction Revival;
    public event UnityAction RewardRevival;

    private void OnEnable()
    {
        _buttonRevival.onClick.AddListener(RevialInvoke);
        _buttonRewardRevival.onClick.AddListener(RewardRevivalInvoke);
    }

    private void OnDisable()
    {
        _buttonRevival.onClick.RemoveListener(RevialInvoke);
        _buttonRewardRevival.onClick.RemoveListener(RewardRevivalInvoke);
    }

    private void RevialInvoke()
    {
        Close();
        Revival?.Invoke();
    }

    private void RewardRevivalInvoke()
    {
        Close();
        RewardRevival?.Invoke();
    }

    public override void Close()
    {
        _root.SetActive(false);
    }

    public override void Open()
    {
        _root.SetActive(true);
    }

    public void OnPlayerDie()
    {
        Open();
    }
}
