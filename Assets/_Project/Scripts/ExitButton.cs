using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(ReturnToMainMenu);
        }
        private void OnDisable()
        { 
            _exitButton.onClick.RemoveListener(ReturnToMainMenu);
        }
    
        private void ReturnToMainMenu()
        {
            GameManager.Instance.ClearFields();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
