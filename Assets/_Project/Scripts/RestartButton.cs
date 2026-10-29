using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class RestartButton : MonoBehaviour
    {
        private const float NormalTime = 1f;

        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(ReloadScene);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(ReloadScene);
        }

        private void ReloadScene()
        {
            UIManager.SetSkipMenu();
            //GameManager.Instance.ClearFields();
            Time.timeScale = NormalTime;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
