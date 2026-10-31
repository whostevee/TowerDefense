using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class RestartButton : MonoBehaviour
    {
        private const float NormalTime = 1f;

        [SerializeField] private Button _restartButton;
        [SerializeField] private RestartEvent _restartEvent;

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
            _restartEvent.Restart();
            Time.timeScale = NormalTime;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
