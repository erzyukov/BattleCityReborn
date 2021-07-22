using UnityEngine;

namespace BS
{
    /// <summary>
    /// Усиление уничтожения всех врагов на карте
    /// </summary>
    public class PowerUpDestroyer : IPowerUp
    {
        private EnemySpawner _enemySpawner;

        public PowerUpDestroyer(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void PickUp(GameObject target)
        {
            var enemies = _enemySpawner.Enemies.ToArray();
            for(var i = 0; i < enemies.Length; i++)
                if (enemies[i] != null)
                    enemies[i].ForcedKill();
        }
    }
}