using UnityEngine;

namespace BS
{
    /// <summary>
    /// Усиление бессмертия.
    /// Делает игрока бессмертным на заданное время.
    /// </summary>
    public class PowerUpImmortal : IPowerUp
    {
        private float _duration;

        public PowerUpImmortal(float duration)
        {
            _duration = duration;
        }

        public void PickUp(GameObject target)
        {
            var player = target.GetComponent<Player>();
            player.SetPlayerImmortal(_duration);
        }
    }
}