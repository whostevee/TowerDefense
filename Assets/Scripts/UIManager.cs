using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private HUD _hud;
    [SerializeField] private Button _resumeButton;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(HideMenu);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(HideMenu);
    }
    
    private void HideMenu()
    {
        _menuCanvas.gameObject.SetActive(false);
        _hud.gameObject.SetActive(true);
    }
}
