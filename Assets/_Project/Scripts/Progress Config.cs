using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "ProgressConfig", menuName = "Scriptable Objects/ProgressConfig")]
    public class ProgressConfig : ScriptableObject
    {
        [field: SerializeField] public int AddEnemies { get; private set; }
        [field: SerializeField] public int MaxWaves { get; private set; }
        [field: SerializeField] public int BaseEnemiesPerWave { get; private set; }
        [field: SerializeField] public int WaveIncrease { get; private set; }
        [field: SerializeField] public int BaseEnemyIncrease { get; private set; }
        [field: SerializeField] public int AddEnemiesIncrease { get; private set; }

        public int GetMaxWaves(int level)
        {
            return MaxWaves + level * WaveIncrease;
        }

        public int GetEnemiesPerWave(int level)
        {
            return BaseEnemiesPerWave + level * BaseEnemyIncrease;
        }
        
        public int GetAddEnemies(int level)
        {
            return AddEnemies + level * AddEnemiesIncrease;
        }
    }
}