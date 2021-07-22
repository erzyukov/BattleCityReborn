using System;
using UnityEngine;

namespace BS
{

    /// <summary>
    /// Компонент отвечающий за направление движения противника
    /// </summary>
    public class EnemyMoveDirection : MonoBehaviour, IDirection
    {
        /// <summary>
        /// Направление движения
        /// </summary>
        public Vector2 Direction { get; private set; } = Vector2.down;
        
        /// <summary>
        /// Может ли враг двигаться
        /// </summary>
        public bool CanMove { get; set; } = true;

        private Settings _settings;
        private float _delay;

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Enemy.Direction;
            ResetDeley();
        }

        private void Update()
        {
            if (_delay <= 0 && CanMove)
            {
                Direction = Utils.Random.GetRandomDirection(Direction);
                ResetDeley();
            }
            else
            {
                _delay -= Time.deltaTime;
            }
        }

        private void ResetDeley(bool isCollision = false)
        {
            _delay = (!isCollision)
                ? UnityEngine.Random.Range(_settings.MinTurnDelay, _settings.MaxTurnDelay)
                : UnityEngine.Random.Range(_settings.MinCollisionTurnDelay, _settings.MaxCollisionTurnDelay);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (
                collision.gameObject.CompareTag("Wall") || 
                collision.gameObject.CompareTag("Boundary") || 
                collision.gameObject.CompareTag("Enemy"))
            {
                ResetDeley(true);
            }
        }

        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Минимальное время задержки до поворота
            /// </summary>
            public float MinTurnDelay { get { return minTurnDelay; } }
            /// <summary>
            /// Максимальное время задержки до поворота
            /// </summary>
            public float MaxTurnDelay { get { return maxTurnDelay; } }
            /// <summary>
            /// Минимальное время задержки до поворота при столкновении
            /// </summary>
            public float MinCollisionTurnDelay { get { return minCollisionTurnDelay; } }
            /// <summary>
            /// Максимальное время задержки до поворота при столкновении
            /// </summary>
            public float MaxCollisionTurnDelay { get { return maxCollisionTurnDelay; } }

            [Tooltip("Минимальное время задержки до поворота")]
            [SerializeField]
            private float minTurnDelay;
            [Tooltip("Максимальное время задержки до поворота")]
            [SerializeField]
            private float maxTurnDelay;
            [Tooltip("Минимальное время задержки до поворота при столкновении")]
            [SerializeField]
            private float minCollisionTurnDelay;
            [Tooltip("Максимальное время задержки до поворота при столкновении")]
            [SerializeField]
            private float maxCollisionTurnDelay;
        }
    }
}