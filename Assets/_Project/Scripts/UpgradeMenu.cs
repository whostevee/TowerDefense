using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class UpgradeMenu : MonoBehaviour
    {
        public enum UpgradeType
        {
            Health,
            AttackSpeed,
            Damage
        }
    
        private const string HealthName = "Здоровье";
        private const string AttackSpeedName = "Скорость атаки";
        private const string DamageName = "Сила атаки";
    
        [SerializeField] private TowerConfig _towerConfig;

        [SerializeField] private Button _upgradeHealthButton;
        [SerializeField] private Button _upgradeAttackSpeedButton;
        [SerializeField] private Button _upgradeDamageButton;

        [SerializeField] private TextMeshProUGUI _upgradeHealthText;
        [SerializeField] private TextMeshProUGUI _upgradeSpeedText;
        [SerializeField] private TextMeshProUGUI _upgradeDamageText;

        private int _currentHealthUpgrade;
        private int _currentSpeedUpgrade;
        private int _currentDamageUpgrade;

        private int _maxHealthUpgrade;
        private int _maxSpeedUpgrade;
        private int _maxDamageUpgrade;

        private int _healthUpgradePrice;
        private int _attackSpeedUpgradePrice;
        private int _damageUpgradePrice;
    
        public event Action<UpgradeType> OnTowerUpgraded;

        private void OnEnable()
        {
            _upgradeHealthButton.onClick.AddListener(TakeHealthUpgrade);
            _upgradeAttackSpeedButton.onClick.AddListener(TakeAttackSpeedUpgrade);
            _upgradeDamageButton.onClick.AddListener(TakeDamageUpgrade);
            GameManager.Instance.OnMoneyChanged += RefreshUI;
            UIManager.OnUpgradeMenuOpened += RefreshUI;
        }

        private void OnDisable()
        {
            _upgradeHealthButton.onClick.RemoveListener(TakeHealthUpgrade);
            _upgradeAttackSpeedButton.onClick.RemoveListener(TakeAttackSpeedUpgrade);
            _upgradeDamageButton.onClick.RemoveListener(TakeDamageUpgrade);
            GameManager.Instance.OnMoneyChanged -= RefreshUI;
            UIManager.OnUpgradeMenuOpened -= RefreshUI;
        }

        private void Awake()
        {
            _maxHealthUpgrade = _towerConfig.MaxHealthUpgrade;
            _maxSpeedUpgrade = _towerConfig.MaxAttackSpeedUpgrade;
            _maxDamageUpgrade = _towerConfig.MaxDamageUpgrade;

            _healthUpgradePrice = _towerConfig.HealthUpgradePrice;
            _attackSpeedUpgradePrice = _towerConfig.AttackSpeedUpgradePrice;
            _damageUpgradePrice = _towerConfig.DamageUpgradePrice;
        }

        private void Start()
        {
            RefreshUI();
        }
    
        private string SetUpgradeText(string name, int currentUpgrade, int maxUpgrade, int price)
        {
            string text = $"Улучшение: {name}. {currentUpgrade}/{maxUpgrade}.\nСтоимость {price}";
            return text;
        }

        private void TakeHealthUpgrade()
        {
            GameManager.Instance.BuyUpgrade(_healthUpgradePrice);
            _currentHealthUpgrade++;
            _healthUpgradePrice = _towerConfig.GetNewHealthPrice(_currentHealthUpgrade);
            OnTowerUpgraded?.Invoke(UpgradeType.Health);
            RefreshUI();
        }

        private void TakeDamageUpgrade()
        {
            GameManager.Instance.BuyUpgrade(_damageUpgradePrice);
            _currentDamageUpgrade++;
            _damageUpgradePrice = _towerConfig.GetNewDamagePrice(_currentDamageUpgrade);
            OnTowerUpgraded?.Invoke(UpgradeType.Damage);
            RefreshUI();
        }

        private void TakeAttackSpeedUpgrade()
        {
            GameManager.Instance.BuyUpgrade(_attackSpeedUpgradePrice);
            _currentSpeedUpgrade++;
            _attackSpeedUpgradePrice = _towerConfig.GetNewAttackSpeedPrice(_currentSpeedUpgrade);
            OnTowerUpgraded?.Invoke(UpgradeType.AttackSpeed);
            RefreshUI();
        }

        private void RefreshUI()
        {
            _upgradeHealthText.text =
                SetUpgradeText(HealthName, _currentHealthUpgrade, _maxHealthUpgrade, _healthUpgradePrice);
            _upgradeSpeedText.text = SetUpgradeText(AttackSpeedName, _currentSpeedUpgrade, _maxSpeedUpgrade,
                _attackSpeedUpgradePrice);
            _upgradeDamageText.text =
                SetUpgradeText(DamageName, _currentDamageUpgrade, _maxDamageUpgrade, _damageUpgradePrice);
        
            ActivateButton(_upgradeHealthButton, _healthUpgradePrice, _currentHealthUpgrade, _maxHealthUpgrade);
            ActivateButton(_upgradeAttackSpeedButton, _attackSpeedUpgradePrice, _currentSpeedUpgrade, _maxSpeedUpgrade);
            ActivateButton(_upgradeDamageButton, _damageUpgradePrice, _currentDamageUpgrade, _maxDamageUpgrade);
        }

        private void ActivateButton(Button button, int upgradePrice, int currentUpgrade, int maxUpgrade)
        {
            button.interactable = false;
            if (GameManager.Instance.Money >= upgradePrice && currentUpgrade < maxUpgrade)
            {
                button.interactable = true;
            }
        }
    }
}