using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Scriptable Objects/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        private const float DamageMultiplier = 3f;
        private const float HealthMultiplier = 2f;
        private const float AttackSpeedMultiplier = 3.5f;
        private const int Rounding = 10;
        
        [field: SerializeField] public int TowerMaxHealth {get; private set;}
        [field: SerializeField] public int TowerDamage {get; private set;}
        [field: SerializeField] public float TowerAttackRange {get; private set;}
        [field: SerializeField] public float TowerAttackSpeed {get; private set;}
        [field: SerializeField] public float ProjectileSpeed {get; private set;}
    
        [field:SerializeField] public int HealthUpgradeQuantity {get; private set;}
        [field:SerializeField] public float AttackSpeedUpgradeQuantity {get; private set;}
        [field:SerializeField] public int DamageUpgradeQuantity {get; private set;}
    
        [field: SerializeField] public int MaxDamageUpgrade {get; private set;}
        [field: SerializeField] public int MaxAttackSpeedUpgrade {get; private set;}
        [field: SerializeField] public int MaxHealthUpgrade {get; private set;}
        [field: SerializeField] public int DamageUpgradePrice {get; private set;}
        [field: SerializeField] public int HealthUpgradePrice {get; private set;}
        [field: SerializeField] public int AttackSpeedUpgradePrice {get; private set;}
    
        public int GetNewDamagePrice(int currentLevel)
        {
            float raw = DamageUpgradePrice * Mathf.Pow(DamageMultiplier, currentLevel);
            return Mathf.CeilToInt(raw / Rounding) * Rounding;
        }
    
        public int GetNewHealthPrice(int currentLevel)
        {
            float raw = HealthUpgradePrice * Mathf.Pow(HealthMultiplier, currentLevel);
            return Mathf.CeilToInt(raw / Rounding) * Rounding;
        }
    
        public int GetNewAttackSpeedPrice(int currentLevel)
        {
            float raw = AttackSpeedUpgradePrice * Mathf.Pow(AttackSpeedMultiplier, currentLevel);
            return Mathf.CeilToInt(raw / Rounding) * Rounding;
        }
    }
}
