using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public enum UpgradeType
    {
        Health,
        AttackSpeed,
        Damage
    }
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
    }

    private void OnDisable()
    {
        _upgradeHealthButton.onClick.RemoveListener(TakeHealthUpgrade);
        _upgradeAttackSpeedButton.onClick.RemoveListener(TakeAttackSpeedUpgrade);
        _upgradeDamageButton.onClick.RemoveListener(TakeDamageUpgrade);
    }

    private void Update()
    {
        _upgradeHealthText.text =
            SetUpgradeText("здоровья", _currentHealthUpgrade, _maxHealthUpgrade, _healthUpgradePrice);
        _upgradeSpeedText.text = SetUpgradeText("скорость атаки", _currentSpeedUpgrade, _maxSpeedUpgrade,
            _attackSpeedUpgradePrice);
        _upgradeDamageText.text =
            SetUpgradeText("атаки", _currentDamageUpgrade, _maxDamageUpgrade, _damageUpgradePrice);

        ActivateButton(_upgradeHealthButton, _healthUpgradePrice, _currentHealthUpgrade, _maxHealthUpgrade);
        ActivateButton(_upgradeAttackSpeedButton, _attackSpeedUpgradePrice, _currentSpeedUpgrade, _maxSpeedUpgrade);
        ActivateButton(_upgradeDamageButton, _damageUpgradePrice, _currentDamageUpgrade, _maxDamageUpgrade);
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

    private string SetUpgradeText(string name, int currentUpgrade, int maxUpgrade, int price)
    {
        string text = "Улучшение: " + name + ". " + currentUpgrade + "/" + maxUpgrade + ". Стоимость: " + price;
        return text;
    }

    private void TakeHealthUpgrade()
    {
        GameManager.Instance.BuyUpgrade(_healthUpgradePrice);
        _currentHealthUpgrade++;
        OnTowerUpgraded?.Invoke(UpgradeType.Health);
    }

    private void TakeDamageUpgrade()
    {
        GameManager.Instance.BuyUpgrade(_damageUpgradePrice);
        _currentDamageUpgrade++;
        _damageUpgradePrice = _towerConfig.GetNewDamagePrice(_currentDamageUpgrade);
        OnTowerUpgraded?.Invoke(UpgradeType.Damage);
    }

    private void TakeAttackSpeedUpgrade()
    {
        GameManager.Instance.BuyUpgrade(_attackSpeedUpgradePrice);
        _currentSpeedUpgrade++;
        OnTowerUpgraded?.Invoke(UpgradeType.AttackSpeed);
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