using UnityEngine;
using UnityEngine.UI;

namespace YaAssets
{
    public class LeaderboardButton : MonoBehaviour
    {
        private LeaderboardPanel _panel;
        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void Start() => _panel = LeaderboardPanel.Instance;

        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick() => _panel.Open();
    }
}