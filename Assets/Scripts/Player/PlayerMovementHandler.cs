using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за передвижение игрока
    /// </summary>
    public class PlayerMovementHandler : BaseMovementHandler
    {
        private Settings _settings;

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Player.Movement;
        }

        protected override void Move()
        {
            AdaptPositionToGrid();

            var speed = _dir.Direction * _settings.Speed;
            _rb.velocity = speed;
        }

        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Скорость передвижения игрока
            /// </summary>
            public float Speed { get { return speed; } }

            [Tooltip("Скорость передвижения игрока")]
            [SerializeField]
            private float speed;
        }
    }
}