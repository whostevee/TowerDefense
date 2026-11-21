using _Project.Scripts.Back;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        private Health _health;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Initialize(Health health)
        {
            _health = health;
            health.OnHealthChanged += HealthChanged;
            HealthChanged(_health.CurrentHealth, _health.MaxHealth);
        }

        private void HealthChanged(int currentHealth, int maxHealth)
        {
            _fillImage.fillAmount = (float)currentHealth / maxHealth;
        }

        private void LateUpdate()
        {
            transform.LookAt(_camera.transform.position);
        }

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnHealthChanged -= HealthChanged;
            }
        }
    }
}