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
        [field: SerializeField] public int BaseEnemiesIncrease { get; private set; }
        [field: SerializeField] public int AddEnemiesIncrease { get; private set; }
    }
}