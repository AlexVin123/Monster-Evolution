using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class TimerBoster : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerView;

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

        while (time != 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            _timerView.text = time.ToString();
        }

        gameObject.SetActive(false);
    }
}
