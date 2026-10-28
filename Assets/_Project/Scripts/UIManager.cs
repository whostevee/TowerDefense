using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class UIManager : MonoBehaviour
    {
        private const string OpenMenu = "OpenMenu";
        private const string CloseMenu = "CloseMenu";

        [SerializeField] private Animator _upgradeMenuAnimator;

        [SerializeField] private GameObject _pauseMenuPanel;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _upgradeMenuPanel;
        [SerializeField] private GameObject _loseMenuPanel;
        [SerializeField] private GameObject _winMenuPanel;
        [SerializeField] private GameObject _statsPanel;
        [SerializeField] private HUD _hud;

        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _startButton;

        [SerializeField] private TextMeshProUGUI _statsText;

        public static Action OnGameStarted;
        public static Action OnUpgradeMenuOpened;
        private static bool _skipMenu;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(HideMenu);
            _startButton.onClick.AddListener(StartGame);

            Tower.OnTowerClicked += ShowUpgradeMenu;
            Tower.OnTowerDestroyed += ShowLoseMenu;
            WaveManager.OnWavesEnded += ShowWinMenu;
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(HideMenu);
            _startButton.onClick.RemoveListener(StartGame);

            Tower.OnTowerClicked -= ShowUpgradeMenu;
            Tower.OnTowerDestroyed -= ShowLoseMenu;
            WaveManager.OnWavesEnded -= ShowWinMenu;
        }

        private void Start()
        {
            if (_skipMenu)
            {
                StartGame();
                _skipMenu = false;
            }
            else
            {
                GameManager.Instance.PauseGame();
                _mainMenuPanel.SetActive(true);
            }
        }

        private void HideMenu()
        {
            _pauseMenuPanel.SetActive(false);

            GameManager.Instance.ResumeGame();
            _hud.gameObject.SetActive(true);
        }

        private void StartGame()
        {
            GameManager.Instance.ResumeGame();
            OnGameStarted?.Invoke();
            _mainMenuPanel.SetActive(false);
        }

        private void ShowUpgradeMenu()
        {
            if (_upgradeMenuPanel.activeSelf)
            {
                StartCoroutine(CloseUpgradeMenuRoutine());
            }
            else
            {
                StartCoroutine(OpenUpgradeMenuRoutine());
            }
        }

        private void ShowLoseMenu()
        {
            _hud.gameObject.SetActive(false);
            StartCoroutine(ShowMenuRoutine(_loseMenuPanel, 2f));
        }

        private void ShowWinMenu()
        {
            _hud.gameObject.SetActive(false);
            StartCoroutine(ShowMenuRoutine(_winMenuPanel, 2f));
        }

        private IEnumerator ShowMenuRoutine(GameObject menu, float duration)
        {
            yield return new WaitForSeconds(duration);
            menu.SetActive(true);
            ShowStats();
        }

        private IEnumerator OpenUpgradeMenuRoutine()
        {
            _upgradeMenuPanel.SetActive(true);
            OnUpgradeMenuOpened?.Invoke();
            yield return null;
            _upgradeMenuAnimator.SetTrigger(OpenMenu);
        }

        private IEnumerator CloseUpgradeMenuRoutine()
        {
            _upgradeMenuAnimator.SetTrigger(CloseMenu);
            yield return new WaitForSeconds(0.5f);
            _upgradeMenuPanel.SetActive(false);
        }

        public static void SetSkipMenu()
        {
            _skipMenu = !_skipMenu;
        }

        private void ShowStats()
        {
            _statsPanel.SetActive(true);
            _statsText.text = $"Убито врагов: {StatsManager.Instance.EnemyKilled}\n" +
                              $"Пережито волн: {StatsManager.Instance.WavesSurvived}";
        }
    }
}