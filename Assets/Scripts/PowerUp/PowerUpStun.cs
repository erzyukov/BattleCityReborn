using UnityEngine;

namespace BS
{
    /// <summary>
    /// Усиление остановки противников.
    /// Станит врагов на заданное время.
    /// </summary>
    public class PowerUpStun : IPowerUp
    {
        private float _duration;
        private EnemySpawner _enemySpawner;

        public PowerUpStun(EnemySpawner enemySpawner, float duration)
        {
            _duration = duration;
            _enemySpawner = enemySpawner;
        }

        public void PickUp(GameObject target)
        {
            _enemySpawner.StunEnemies(_duration);
        }
    }
}