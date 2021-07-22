using System;
using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Спавнер врагов
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [Tooltip("Точки спавна противника")]
        [SerializeField]
        private Transform[] spawnPoints;

        /// <summary>
        /// Список врагов находящихся в данный момент на карте
        /// </summary>
        public List<Enemy> Enemies { get { return _enemies; } }
        /// <summary>
        /// Событие вызываемое когда уничтожен бонусный противник
        /// </summary>
        public Action OnBonusEnemyDestroyed;
        /// <summary>
        /// Событие вызываемое при уничтожении противника
        /// </summary>
        public Action OnEnemySpawn;

        private Settings _settings;

        private PoolingService<Enemy> _spawner;
        private List<Enemy> _enemies;

        private float _spawnDelay;
        private int _nextSpawnNumber;
        private int _maxAmountOnMap;
        private int _currentBonusIndex;

        private float _stunRemain = 0;

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Enemy.Spawner;
            _spawner = new PoolingService<Enemy>(_settings.Prefab, 8, transform, true);
            _enemies = new List<Enemy>();
            _maxAmountOnMap = (GameState.Instance.CountType == GameState.GameType.One) 
                ? _settings.MaxAmountWithOnePlayer 
                : _settings.MaxAmountWithTwoPlayers;
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange += GameStateChangeHandle;
        }

        private void Update()
        {
            // если прошла задержка, количество врагов на карте меньше заданного и не достигнуто максимальное значение врагов
            if (_spawnDelay <= 0 && _enemies.Count < _maxAmountOnMap && _nextSpawnNumber < _settings.EnemyList.Length)
            {
                // спавним врага
                var enemy = SpawnEnemy();
                _enemies.Add(enemy);

                // если враги под станом и появился новый, то он тоже должен отдохнуть
                if (_stunRemain > 0)
                    enemy.Stun(_stunRemain);

                _nextSpawnNumber++;
                _spawnDelay = _settings.SpawnDelay;
            }
            else if (_enemies.Count < _maxAmountOnMap)
            {
                // если врагов на карте больше заданного то время задержки не уменьшаем
                _spawnDelay -= Time.deltaTime;
            }
            if (_stunRemain > 0)
                _stunRemain -= Time.deltaTime;
        }

        /// <summary>
        /// Станит всех врагов на карте, на заданное время
        /// </summary>
        public void StunEnemies(float duration)
        {
            _stunRemain = duration;
            foreach(var enemy in Enemies)
                enemy.Stun(duration);
        }

        /// <summary>
        /// Спавнит врага
        /// </summary>
        private Enemy SpawnEnemy()
        {
            var enemy = _spawner.GetFreeElement();
            var isBonus = false;
            if (_currentBonusIndex < _settings.BonusEnemyNumbers.Length && 
                _settings.BonusEnemyNumbers[_currentBonusIndex] == _nextSpawnNumber + 1)
            {
                _currentBonusIndex++;
                isBonus = true;
            }

            enemy.Init(
                spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position,
                _settings.EnemyList[_nextSpawnNumber],
                isBonus
            );

            OnEnemySpawn?.Invoke();

            enemy.OnDestroyed += OnDestroyedHandler;

            return enemy;
        }
        private void GameStateChangeHandle(GameState.StateType state)
        {
            if (state == GameState.StateType.Score && _enemies.Count > 0)
            {
                _nextSpawnNumber = _settings.EnemyList.Length;
                foreach(var enemy in _enemies)
                    enemy.ReturnToPool();
            }
        }

        /// <summary>
        /// Обрабатывает событие уничтожения врага
        /// </summary>
        /// <param name="enemy"></param>
        private void OnDestroyedHandler(Enemy enemy)
        {
            _enemies.Remove(enemy);

            enemy.OnDestroyed -= OnDestroyedHandler;

            if (enemy.IsBonusEnemy)
                OnBonusEnemyDestroyed?.Invoke();

            if (_enemies.Count <= 0 && _nextSpawnNumber >= _settings.EnemyList.Length)
                GameState.Instance.SetState(GameState.StateType.Win);
        }

        private void OnDestroy()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange -= GameStateChangeHandle;
        }

        #region Enemy Spawner Settings
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Префаб врага
            /// </summary>
            public Enemy Prefab { get { return prefab; } }

            /// <summary>
            /// Задержка перед спауном врага
            /// </summary>
            public int SpawnDelay { get { return spawnDelay; } }

            /// <summary>
            /// Максимальное количество врагов с одном игроком
            /// </summary>
            public int MaxAmountWithOnePlayer { get { return maxAmountWithOnePlayer; } }

            /// <summary>
            /// Максимальное количество врагов с двумя игроками
            /// </summary>
            public int MaxAmountWithTwoPlayers { get { return maxAmountWithTwoPlayers; } }

            /// <summary>
            /// Номера бонусных врагов
            /// </summary>
            public int[] BonusEnemyNumbers { get { return bonusEnemyNumbers; } }

            /// <summary>
            /// Список врагов на уровень
            /// </summary>
            public EnemySettings[] EnemyList { get { return enemyList; } }

            [Tooltip("Префаб врага")]
            [SerializeField]
            private Enemy prefab;

            [Tooltip("Задержка перед спауном врага")]
            [SerializeField]
            private int spawnDelay;

            [Tooltip("Максимальное количество врагов с одном игроком")]
            [SerializeField]
            private int maxAmountWithOnePlayer;

            [Tooltip("Максимальное количество врагов с двумя игроками")]
            [SerializeField]
            private int maxAmountWithTwoPlayers;

            [Tooltip("Номера бонусных врагов")]
            [SerializeField]
            private int[] bonusEnemyNumbers;

            // пусть будет здесь, все равно будет только один уровень
            [Tooltip("Список врагов на уровень")]
            [SerializeField]
            private EnemySettings[] enemyList;
        }
        #endregion

    }
}
