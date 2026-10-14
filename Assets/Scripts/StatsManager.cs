using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance {get; private set;}

    public int EnemyKilled { get; private set; }
    public int WavesSurvived { get; private set; }

    private void OnEnable()
    {
        Enemy.OnEnemyDied += AddKill;
        WaveManager.OnWaveFinished += AddWave;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDied -= AddKill;
        WaveManager.OnWaveFinished -= AddWave;
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void AddKill(Enemy e)
    {
        EnemyKilled++;
    }

    private void AddWave()
    {
        WavesSurvived++;
    }
    
}
