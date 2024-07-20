using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;
using UnityEngine.UI;

public class BosterWindow : Window
{
    [SerializeField] private GameObject _root;
    [SerializeField] private Button _buttonClouse;
    [SerializeField] private Button _buttonTakeReward;

    public event UnityAction TakedReward;

    private void OnEnable()
    {
        _buttonClouse.onClick.AddListener(Close);
        _buttonTakeReward.onClick.AddListener(OnTakeReward);
    }

    private void OnDisable()
    {
        _buttonClouse.onClick.RemoveListener(Close);
        _buttonTakeReward.onClick.RemoveListener(OnTakeReward);
    }

    private void OnTakeReward()
    {
        Close();
        TakedReward?.Invoke();
    }

    public override void Close()
    {
        base.Close();
        _root.SetActive(false);
    }

    public override void Open()
    {
        base.Open();
        _root.SetActive(true);
    }
}
