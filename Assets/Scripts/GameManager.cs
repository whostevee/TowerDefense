using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Enemy> _enemies;
    public IReadOnlyList<Enemy> Enemies => _enemies;

    public static GameManager Instance { get; private set; }
    private int _money;
    private int _rewardForKill;
    private bool _isGameStarted;
    private int _diedEnemyCount;

    public int DiedEnemyCount => _diedEnemyCount;
    public int Money => _money;

    private void OnEnable()
    {
        Enemy.OnEnemyDied += HandleEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDied -= HandleEnemyDied;
    }

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

    public void ClearFields()
    {
        _enemies.Clear();
        _money = 0;
    }

    public void ClearDiedEnemies()
    {
        _diedEnemyCount = 0;
    }

    private void HandleEnemyDied(Enemy e)
    {
        _rewardForKill = 10;
        _diedEnemyCount++;
        _money += _rewardForKill;

        RemoveEnemy(e);
    }

    public void BuyUpgrade(int price)
    {
        if (_money >= price)
        {
            _money -= price;
        }
    }
}