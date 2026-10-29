using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private const int RewardForKill = 10;
        private const float PausedTime = 0f;
        private const float NormalTime = 1f;

        private List<Enemy> _enemies;
        public IReadOnlyList<Enemy> Enemies => _enemies;

        public static GameManager Instance { get; private set; }
        private int _money;
        private bool _isGameStarted;
        private int _diedEnemyCount;
        private bool _isGamePaused;

        public event Action OnMoneyChanged;
        public event Action OnEnemyValueChanged;

        public int DiedEnemyCount => _diedEnemyCount;
        public int Money => _money;

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

        private void Start()
        {
            Enemy.OnEnemyDied += HandleEnemyDied;
        }

        private void OnDestroy()
        {
            Enemy.OnEnemyDied -= HandleEnemyDied;
        }

        public void AddEnemy(Enemy e)
        {
            _enemies.Add(e);
        }

        private void RemoveEnemy(Enemy e)
        {
            _enemies.Remove(e);
        }

        public void ClearFields()
        {
            _enemies.Clear();
            _money = 0;
            StatsManager.Instance.ClearStats();
        }

        public void ClearDiedEnemies()
        {
            _diedEnemyCount = 0;
            OnEnemyValueChanged?.Invoke();
        }

        private void HandleEnemyDied(Enemy e)
        {
            _money += RewardForKill;
            OnMoneyChanged?.Invoke();

            _diedEnemyCount++;
            OnEnemyValueChanged?.Invoke();

            RemoveEnemy(e);
        }

        public void BuyUpgrade(int price)
        {
            if (_money < price)
            {
                return;
            }

            if (_money >= price)
            {
                _money -= price;
                OnMoneyChanged?.Invoke();
            }
        }

        public void PauseGame()
        {
            if (!_isGamePaused)
            {
                _isGamePaused = true;
                Time.timeScale = PausedTime;
            }
        }

        public void ResumeGame()
        {
            if (_isGamePaused)
            {
                _isGamePaused = false;
                Time.timeScale = NormalTime;
            }
        }
    }
}