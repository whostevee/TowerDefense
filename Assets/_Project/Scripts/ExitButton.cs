using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ExitButton : MonoBehaviour
    {
        private const string MainMenuName = "MainMenu";
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
            SceneManager.LoadScene(MainMenuName);
        }
    }
}
