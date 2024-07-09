using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace YaAssets
{
    public class Startup : MonoBehaviour
    {
        private IEnumerator Start()
        {
            while (YandexGame.SDKEnabled == false)
                yield return null;

            SceneManager.LoadSceneAsync(1);
        }
    }
}