using UnityEngine;

namespace BS
{
    /// <summary>
    /// Поднимает уровень игрока.
    /// </summary>
    public class PowerUpUpgrade : IPowerUp
    {
        public void PickUp(GameObject target)
        {
            var player = target.GetComponent<Player>();
            player.SetNextLevel();
        }
    }
}