using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(ContinueGame);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ContinueGame);
        }        

        private void ContinueGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
