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
        private const float UpgradeMenuAnimationDuration = 0.5f;
        private const float ShowMenuDuration = 2f;

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
        
        
        private Coroutine _upgradeMenuRoutine;
        private readonly WaitForSeconds _upgradeAnimationDelay = new WaitForSeconds(UpgradeMenuAnimationDuration);

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


        private void ShowLoseMenu()
        {
            _hud.gameObject.SetActive(false);
            StartCoroutine(ShowMenuRoutine(_loseMenuPanel, ShowMenuDuration));
        }

        private void ShowWinMenu()
        {
            _hud.gameObject.SetActive(false);
            StartCoroutine(ShowMenuRoutine(_winMenuPanel, ShowMenuDuration));
        }

        private IEnumerator ShowMenuRoutine(GameObject menu, float duration)
        {
            yield return new WaitForSeconds(duration);
            menu.SetActive(true);
            ShowStats();
        }
        
        private void ShowUpgradeMenu()
        {
            if (_upgradeMenuRoutine != null)
            {
                StopCoroutine(_upgradeMenuRoutine);
            }

            _upgradeMenuRoutine = StartCoroutine(_upgradeMenuPanel.activeSelf ? CloseUpgradeMenuRoutine() : OpenUpgradeMenuRoutine());
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
            yield return _upgradeAnimationDelay;
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