using UnityEngine;

namespace BS
{
    /// <summary>
    /// Усиление добавляющее пользователю дополнительную жизнь
    /// </summary>
    public class PowerUpExtraLife : IPowerUp
    {
        public void PickUp(GameObject target)
        {
            var player = target.GetComponent<Player>();
            player.AddExtraLife();
        }
    }
}