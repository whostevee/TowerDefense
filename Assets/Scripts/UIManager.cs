using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator _upgradeMenuAnumator;

    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _upgradeMenuPanel;
    [SerializeField] private GameObject _loseMenuPanel;
    [SerializeField] private GameObject _winMenuPanel;
    [SerializeField] private HUD _hud;

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _startButton;

    public static Action OnGameStarted;
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
            Time.timeScale = 0f;
            _mainMenuPanel.SetActive(true);
        }
    }

    private void HideMenu()
    {
        _pauseMenuPanel.SetActive(false);

        Time.timeScale = 1f;
        _hud.gameObject.SetActive(true);
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        OnGameStarted?.Invoke();
        _mainMenuPanel.SetActive(false);
    }

    private void ShowUpgradeMenu()
    {
        if (_upgradeMenuPanel.activeSelf)
        {
            StartCoroutine(CloseMenuRoutine());
        }
        else
        {
            StartCoroutine(OpenMenuRoutine());
        }
    }

    private void ShowLoseMenu()
    {
        _loseMenuPanel.SetActive(true);
    }

    private void ShowWinMenu()
    {
        _winMenuPanel.SetActive(true);
    }

    private IEnumerator OpenMenuRoutine()
    {
        _upgradeMenuPanel.SetActive(true);
        yield return null;
        _upgradeMenuAnumator.SetTrigger("OpenMenu");
    }

    private IEnumerator CloseMenuRoutine()
    {
        _upgradeMenuAnumator.SetTrigger("CloseMenu");
        yield return new WaitForSeconds(0.5f);
        _upgradeMenuPanel.SetActive(false);
    }

    public static void ChangeFlag()
    {
        if (_skipMenu)
        {
            _skipMenu = false;
        }
        else
        {
            _skipMenu = true;
        }
    }
}