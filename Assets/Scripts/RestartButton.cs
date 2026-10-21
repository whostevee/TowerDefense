using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private void OnEnable() => _restartButton.onClick.AddListener(ReloadScene);
    
    private void OnDisable() => _restartButton.onClick.RemoveListener(ReloadScene);

    private void ReloadScene()
    {
        UIManager.SetSkipMenu();
        GameManager.Instance.ClearFields();
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
