using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field:SerializeField] public int EnemyMaxHealth {get; private set;}
        [field: SerializeField] public float EnemySpeed {get; private set;}
        [field: SerializeField] public int EnemyDamage {get; private set;}
        [field: SerializeField] public int EnemyAttackDelay {get; private set;}
        
        [field: SerializeField] public int MaxWaves {get; private set;}
        [field: SerializeField] public int EnemiesPerWave {get; private set;}
        [field: SerializeField] public float SpawnTime {get; private set;}
        [field: SerializeField] public int WaveDelay {get; private set;}
    }
}
