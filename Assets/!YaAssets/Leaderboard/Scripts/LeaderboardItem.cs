using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace YaAssets
{
    public class LeaderboardItem : MonoBehaviour
    {
        internal CanvasGroup CanvasGroup;
        [SerializeField] private TextMeshProUGUI _textPosition;
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private Image _background;
        [SerializeField] private Color32 _otherPlayerColor;
        [SerializeField] private Color32 _playerColor;
        [SerializeField] private Image _frame;
        [SerializeField] private Color32 _framePlayerColor;
        [SerializeField] private Color32 _frameOtherColor;
        [SerializeField] private Image _subPoints;
        [SerializeField] private Color32 _subPointsPlayerColor;
        [SerializeField] private Color32 _subPointsOtherColor;

        [SerializeField] private int _lengthName;

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetData(int positionPlayer, int scorePlayer, string namePlayer, string id, string playerID)
        {
            if (scorePlayer == 0)
            {
                _textScore.text = _textName.text = string.Empty;
                return;
            }

            _textPosition.text = positionPlayer.ToString();
            _textScore.text = scorePlayer.ToString();

            _textName.text = string.IsNullOrEmpty(namePlayer) || namePlayer == "anonymous"
                ? _textName.text = (YandexGame.EnvironmentData.language == "ru" ? "Скрыт" : "Hidden")
                : namePlayer;


            var isPlayer = playerID == id;
            _background.color = isPlayer ? _playerColor : _otherPlayerColor;
            _frame.color = isPlayer ? _framePlayerColor : _frameOtherColor;
            _subPoints.color = isPlayer ? _subPointsPlayerColor : _subPointsOtherColor;
        }
    }
}