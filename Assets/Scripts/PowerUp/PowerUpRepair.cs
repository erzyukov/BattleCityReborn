using UnityEngine;

namespace BS
{
    /// <summary>
    /// Усиление починки бункера.
    /// Застраивает бункер бетоном. Через некоторое время на место бетона возвращаются кирпичи.
    /// </summary>
    public class PowerUpRepair : IPowerUp
    {
        private float _duration;
        private MapStateHandler _map;

        public PowerUpRepair(MapStateHandler map, float duration)
        {
            _duration = duration;
            _map = map;
        }

        public void PickUp(GameObject target)
        {
            _map.RepairHQ(_duration);
        }
    }
}