using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Enemy> _enemies;
    public IReadOnlyList<Enemy>  Enemies => _enemies;
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        _enemies = new List<Enemy>();
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddEnemy(Enemy e)
    {
        _enemies.Add(e);
    }

    public void RemoveEnemy(Enemy e)
    {
       _enemies.Remove(e);
    }
    
    public void ClearEnemies()
    {
        _enemies.Clear();
    }
}