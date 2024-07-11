using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingWindow : Window
{
    [SerializeField] private GameObject _root;
    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Toggle _buttonSounds;
    [SerializeField] private Toggle _buttonMusics;
    [SerializeField] private Button _open;
    [SerializeField] private Button _close;

    private float _startVolumeSounds;
    private float _startVolumeMusic;

    private void Awake()
    {
        if(_mixer.GetFloat("Music", out float value))
        {
            _startVolumeMusic = value;
        }

        if(_mixer.GetFloat("Sounds", out float value2))
        {
            _startVolumeSounds = value2;
        }
        _buttonMusics.onValueChanged.AddListener(ChangeMusic);
        _buttonSounds.onValueChanged.AddListener(ChangeSounds);
        _open.onClick.AddListener(Open);
        _close.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _buttonMusics.onValueChanged.RemoveListener(ChangeMusic);
        _buttonSounds.onValueChanged.RemoveListener(ChangeSounds);
        _open.onClick.RemoveListener(Open);
        _close.onClick.RemoveListener(Close);
    }

    public override void Open()
    {
        base.Open();
        _root.SetActive(true);
        _open.gameObject.SetActive(false);
    }

    public override void Close() 
    { 
        base.Close();
        _root.SetActive(false);
        _open.gameObject.SetActive(true);
    }

    private void ChangeSounds(bool value)
    {
        if(value == true)
        {
            _mixer.SetFloat("Sounds", _startVolumeSounds);
        }
        else
        {
            _mixer.SetFloat("Sounds", -80);
        }
    }

    private void ChangeMusic(bool value)
    {
        if (value == true)
        {
            _mixer.SetFloat("Music", _startVolumeMusic);
        }
        else
        {
            _mixer.SetFloat("Music", -80);
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Open();
        }
    }
}
