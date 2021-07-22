using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент ведущий подсчет очков набранных игроком
    /// </summary>
    [RequireComponent(typeof(PlayerFireHandler))]
    public class PlayerScoreHandler : MonoBehaviour
    {
        /// <summary>
        /// Количество набранных очков
        /// </summary>
        public int Score { get; private set; }
        /// <summary>
        /// Счетчик убийств
        /// </summary>
        public Dictionary<Enemy.EnemyType, int> KillCounter { get { return _killCouner; } }

        private Dictionary<Enemy.EnemyType, int> _killCouner;

        private PlayerFireHandler _fire;

        private void Awake()
        {
            _fire = GetComponent<PlayerFireHandler>();
            _fire.OnEnemyDestroy += EnemyDestroyHandle;

            _killCouner = new Dictionary<Enemy.EnemyType, int>();
            foreach (Enemy.EnemyType type in (Enemy.EnemyType[])Enum.GetValues(typeof(Enemy.EnemyType)))
                _killCouner[type] = 0;
        }

        /// <summary>
        /// Добавить очки
        /// </summary>
        public void AddScore(int amount)
        {
            Score += amount;
        }

        private void EnemyDestroyHandle(Enemy.EnemyType type)
        {
            _killCouner[type]++;
        }

        private void OnDestroy()
        {
            if (_fire != null)
                _fire.OnEnemyDestroy -= EnemyDestroyHandle;
        }
    }
}