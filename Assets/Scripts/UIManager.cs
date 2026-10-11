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
    [SerializeField] private HUD _hud;

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _startButton;
    

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(HideMenu);
        _restartButton.onClick.AddListener(ReloadScene);
        _exitButton.onClick.AddListener(ReturnToMainMenu);
        _startButton.onClick.AddListener(ReloadScene);

        Tower.OnTowerClicked += ShowUpgradeMenu;
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(HideMenu);
        _restartButton.onClick.RemoveListener(ReloadScene);
        _exitButton.onClick.RemoveListener(ReturnToMainMenu);
        _startButton.onClick.RemoveListener(ReloadScene);

        Tower.OnTowerClicked -= ShowUpgradeMenu;
    }

    private void HideMenu()
    {
        _pauseMenuPanel.SetActive(false);

        Time.timeScale = 1f;
        _hud.gameObject.SetActive(true);
    }

    private void ReturnToMainMenu()
    {
        _pauseMenuPanel.SetActive(false);
        _mainMenuPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private void ReloadScene()
    {
        GameManager.Instance.ClearFields();
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}