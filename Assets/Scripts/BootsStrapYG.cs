using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class BootsStrapYG : MonoBehaviour
{
    
    private IEnumerator Start()
    {
        while (YandexGame.SDKEnabled == false)
        {
            yield return null;
        }

        SceneManager.LoadScene(1);
    }
}
