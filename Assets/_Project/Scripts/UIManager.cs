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
        [SerializeField] private Tower _tower;
        
        [SerializeField] private HUD _hud;
        [SerializeField] private StatsManager _statsManager;
        [SerializeField] private TimeManager _timeManager;
        [SerializeField] private WaveManager _waveManager;

        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _startButton;

        [SerializeField] private TextMeshProUGUI _statsText;
        
        [SerializeField] private RestartEvent _restartEvent;
        
        private Coroutine _upgradeMenuRoutine;
        private readonly WaitForSeconds _upgradeAnimationDelay = new WaitForSeconds(UpgradeMenuAnimationDuration);

        public event Action OnGameStarted;
        public event Action OnUpgradeMenuOpened;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(HideMenu);
            _startButton.onClick.AddListener(StartGame);
            _restartEvent.OnRestart += SetSkipMenu;
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(HideMenu);
            _startButton.onClick.RemoveListener(StartGame);
            _restartEvent.OnRestart -= SetSkipMenu;
        }

        private void Start()
        {
            _tower.OnTowerClicked += ShowUpgradeMenu;
            _tower.OnTowerDestroyed += ShowLoseMenu;
            _waveManager.OnWavesEnded += ShowWinMenu;
            
            if (_restartEvent.SkipMenu)
            {
                StartGame();
                _restartEvent.SkipMenu = false;
            }
            else
            {
                _timeManager.Pause();
                _mainMenuPanel.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            _tower.OnTowerClicked -= ShowUpgradeMenu;
            _tower.OnTowerDestroyed -= ShowLoseMenu;
            _waveManager.OnWavesEnded -= ShowWinMenu;
        }

        private void HideMenu()
        {
            _pauseMenuPanel.SetActive(false);

            _timeManager.Resume();
            _hud.gameObject.SetActive(true);
        }

        private void StartGame()
        {
            _timeManager.Resume();
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

        private void SetSkipMenu()
        {
            _restartEvent.SkipMenu = !_restartEvent.SkipMenu;
        }

        private void ShowStats()
        {
            _statsPanel.SetActive(true);
            _statsText.text = $"Убито врагов: {_statsManager.TotalEnemyKilled}\n" +
                              $"Пережито волн: {_statsManager.WavesSurvived}";
        }
    }
}