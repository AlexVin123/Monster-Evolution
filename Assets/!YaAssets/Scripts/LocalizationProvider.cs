using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YaAssets
{
    public class LocalizationProvider : MonoBehaviour
    {
        [SerializeField] string keyLang;

        private void Start()
        {
            var text = GetComponent<Text>();
            if (text != null)
            {
                text.text = Localization.GetText(keyLang);
                return;
            }

            var tmpText = GetComponent<TMP_Text>();
            if (tmpText != null)
                tmpText.text = Localization.GetText(keyLang);
        }
    }
}