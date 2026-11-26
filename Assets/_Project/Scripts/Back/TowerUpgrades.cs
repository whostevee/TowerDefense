using UnityEngine;

namespace _Project.Scripts.Back
{
    public struct TowerUpgradesSettings
    {
        public float ShotDelayUpgradeQuantity;
        public int HealthUpgradeQuantity;
        public int DamageUpgradeQuantity;
    }

    public class TowerUpgrades
    {
        private readonly TowerUpgradesSettings _towerSettings;
        private readonly Health _health;
        private float _maxShotDelayUpgradeNumber = 0.1f;
        public int Damage { get; private set; }
        public float ShotDelay { get; private set; }

        public TowerUpgrades(TowerUpgradesSettings towerSettings, Health health, float shotDelay, int damage)
        {
            _towerSettings = towerSettings;
            _health = health;
            ShotDelay = shotDelay;
            Damage = damage;
        }

        public void TakeHealthUpgrade()
        {
            _health.IncreaseMaxHealth(_towerSettings.HealthUpgradeQuantity);
        }

        public void TakeDamageUpgrade()
        {
            Damage += _towerSettings.DamageUpgradeQuantity;
        }

        public void TakeAttackSpeedUpgrade()
        {
            ShotDelay -= _towerSettings.ShotDelayUpgradeQuantity;
            ShotDelay = Mathf.Max(ShotDelay, _maxShotDelayUpgradeNumber);
        }
    }
}