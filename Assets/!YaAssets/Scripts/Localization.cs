using System.Collections.Generic;
using UnityEngine;
using YG;

namespace YaAssets
{
    public static class Localization
    {
        private static Dictionary<string, string> _translations;

        public static void InitTranslations()
        {
            if (YandexGame.EnvironmentData.language == "en")
            {
                _translations = new()
                {
                    { "Level", "Level" },
                    { "NEXT", "NEXT" },
                    { "PLAY", "PLAY" },
                    { "SETTINGS", "SETTINGS" },
                    { "CLAIM", "CLAIM" },
                    { "LOADING...", "LOADING..." },
                    { "CONTINUE", "CONTINUE" },
                };
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                _translations = new()
                {
                    { "Level", "Seviye" },
                    { "NEXT", "SONRAKİ" },
                    { "PLAY", "OYNA" },
                    { "SETTINGS", "Ayarlar" },
                    { "Sound", "Ses" },
                    { "Music", "Müzik" },
                    { "Push", "İTMEK" },
                };
            }
            else
            {
                _translations = new()
                {
                    { "Level", "Уровень" },
                    { "NEXT", "ДАЛЬШЕ" },
                    { "PLAY", "ИГРАТЬ" },
                    { "SETTINGS", "НАСТРОЙКИ" },
                    { "CLAIM", "ЗАБРАТЬ" },
                    { "LOADING...", "ЗАГРУЗКА..." },
                    { "CONTINUE", "ПРОДОЛЖИТЬ" },
                };
            }
        }

        public static string GetText(string key)
        {
            if (_translations.ContainsKey(key))
            {
                return _translations[key];
            }
            else
            {
                Debug.LogError($"Translation for {key} not found.");
                return key;
            }
        }
    }
}