using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за движение врага
    /// </summary>
    public class EnemyMovementHandler : BaseMovementHandler
    {
        public float Speed { get; set; }

        protected override void Move()
        {
            if (Speed == 0)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            AdaptPositionToGrid();

            var speed = _dir.Direction * Speed;
            _rb.velocity = speed;
        }
    }
}