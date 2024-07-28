using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerBoster : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerView;
    [SerializeField] private Image _image;

    private float _time;

    public void StartTimer(float time)
    {
        gameObject.SetActive(true);
        _time = time;
        StartCoroutine(Timer());

    }

    private IEnumerator Timer()
    {
        float time = _time;
        _timerView.text = time.ToString();
        _image.fillAmount = 1;

        while (time != 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            _timerView.text = time.ToString();
            _image.fillAmount = time/_time;
        }

        gameObject.SetActive(false);
    }
}
