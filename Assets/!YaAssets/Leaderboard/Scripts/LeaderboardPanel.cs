using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

namespace YaAssets
{
    public class LeaderboardPanel : MonoBehaviour
    {
        public static LeaderboardPanel Instance;

        [SerializeField] private LeaderboardItem[] _leaderboardItems;
        [SerializeField] private GameObject _leaderboardPanel;
        [SerializeField] private GameObject _bg;
        [SerializeField] private Button _buttonClose;
        [SerializeField] private AnimationCurve _animationCurvePanelScale;
        [SerializeField, Range(1f, 10f)] private float _animationSpeedPanelScale;
        [SerializeField] private AnimationCurve _animationCurveItemScale;
        [SerializeField, Range(1f, 10f)] private float _animationSpeedItem;

        private readonly int _limit = 8;
        private Coroutine _showItemCoroutine;
        private bool _isHide;
        private string _playerID;
        private readonly string _lbName = "Leaderboard";

        private void Awake() => Instance = this;

        private void Start()
        {
            GetPlayerID();
        }

        private void GetPlayerID()
        {
            _playerID = YandexGame.playerId;
        }

        public void Open()
        {
            _isHide = false;
            StopAllCoroutines();
            Fetch();
            TogglePanel(true);
        }

        private void Close()
        {
            _isHide = true;
            StopAllCoroutines();
            foreach (var item in _leaderboardItems)
            {
                HideItem(item);
            }

            TogglePanel(false);
        }

        private void TogglePanel(bool enable)
        {
            _leaderboardPanel.SetActive(enable);
            StartCoroutine(OpenAnimation());
        }

        public void Fetch() => YandexGame.GetLeaderboard(_lbName, _limit, 3, 3, "nonePhoto");


        private void OnEnable()
        {
            YandexGame.onGetLeaderboard += OnFetchSuccessHandler;
            //_buttonOpen.onClick.AddListener(Open);
            _buttonClose.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            YandexGame.onGetLeaderboard -= OnFetchSuccessHandler;
            _buttonClose.onClick.RemoveAllListeners();
        }

        private void OnFetchSuccessHandler(LBData data)
        {
            if (data.entries == "initialized")
            {
                Fetch();
            }
            else if (data.technoName == _lbName)
            {
                var players = data.players;

                for (int i = 0, y = 0; i < players.Length; i++, y++)
                {
                    if (players.Length > _limit && i == _limit - 1)
                    {
                        y--;
                        continue;
                    }

                    _leaderboardItems[y].SetData(
                        players[i].rank,
                        players[i].score,
                        players[i].name,
                        players[i].uniqueID,
                        _playerID);
                }
            }
        }

        #region ANIMATION

        IEnumerator OpenAnimation()
        {
            _bg.transform.localScale = Vector3.one * 0.7f;
            var timerAnim = 0f;

            while (timerAnim != 1f)
            {
                timerAnim = Mathf.MoveTowards(timerAnim, 1f, _animationSpeedPanelScale * Time.deltaTime);
                _bg.transform.localScale = Vector3.one *
                                           _animationCurvePanelScale.Evaluate(timerAnim);
                yield return null;
            }

            yield return new WaitWhile((() => _bg.transform.localScale != Vector3.one));


            foreach (var item in _leaderboardItems)
            {
                if (_showItemCoroutine != null)
                {
                    StopCoroutine(_showItemCoroutine);
                    _showItemCoroutine = null;
                }

                if (!_isHide)
                    _showItemCoroutine = StartCoroutine(ShowItem(item));

                yield return _showItemCoroutine;
            }
        }

        void HideItem(LeaderboardItem leaderboardItem)
        {
            var canvasGroup = leaderboardItem.CanvasGroup;
            canvasGroup.alpha = 0f;
        }


        IEnumerator ShowItem(LeaderboardItem leaderboardItem)
        {
            var canvasGroup = leaderboardItem.CanvasGroup;
            var timerAnim = 0f;
            while (canvasGroup.alpha != 1 & timerAnim != 1f)
            {
                timerAnim = Mathf.MoveTowards(timerAnim, 1f, _animationSpeedItem * Time.deltaTime);
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, _animationSpeedItem * Time.deltaTime);
                leaderboardItem.transform.localScale = Vector3.one * _animationCurveItemScale.Evaluate(timerAnim);
                yield return null;
            }
        }

        #endregion
    }

    [Serializable]
    public class LeaderboardData
    {
        public int id;
        public int score;
        public string name;
        public int position;
    }
}