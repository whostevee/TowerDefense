using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private MonoBehaviour _target;

    private IDamageable _damageable;


    private void Start()
    {
        _damageable = _target as IDamageable;
    }

    private void Update()
    {
        if (_damageable != null)
        {
            _fillImage.fillAmount = (float)_damageable.Health / _damageable.MaxHealth;
        }
    }
}