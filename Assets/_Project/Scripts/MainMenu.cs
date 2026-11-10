using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
   public class MainMenu : MonoBehaviour
   {
      private const string SceneName = "_TowerDefense";
      [SerializeField] private Button _startGameButton;

      private void OnEnable()
      {
         _startGameButton.onClick.AddListener(StartGame);
      }

      private void OnDisable()
      {
         _startGameButton.onClick.RemoveListener(StartGame);
      }

      private void StartGame()
      {
         SceneManager.LoadScene(SceneName);
      }
   }
}
