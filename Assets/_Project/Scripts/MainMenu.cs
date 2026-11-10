using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   private const string SceneName = "_TowerDefense";
   [SerializeField] private Button _startGameButton;

   private void Start()
   {
      _startGameButton.onClick.AddListener(StartGame);
   }

   private void OnDestroy()
   {
      _startGameButton.onClick.RemoveListener(StartGame);
   }

   private void StartGame()
   {
      SceneManager.LoadScene(SceneName);
   }
}
