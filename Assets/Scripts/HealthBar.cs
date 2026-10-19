using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private MonoBehaviour _target;

    private IDamageable _damageable;
    private Camera _camera;

    private void Start()
    {
        _damageable = _target as IDamageable;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_damageable != null)
        {
            _fillImage.fillAmount = (float)_damageable.Health / _damageable.MaxHealth;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(_camera.transform.position);
    }
}