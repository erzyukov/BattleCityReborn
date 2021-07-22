using System;
using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент стрельбы игрока
    /// </summary>
    [RequireComponent(typeof(IFire))]
    public class PlayerFireHandler : BaseFireHandler
    {
        /// <summary>
        /// Тип пули
        /// </summary>
        public Bullet.Type BulletType { get; set; } = Bullet.Type.Simple;

        /// <summary>
        /// Событие возникающее при убийстве врага
        /// </summary>
        public Action<Enemy.EnemyType> OnEnemyDestroy;

        /// <summary>
        /// Событие возникающее при выстреле
        /// </summary>
        public Action OnFire;

        private IFire _fire;

        private Settings _settings;

        private float _currentBulletSpeed;

        protected override void Awake()
        {
            base.Awake();
            _fire = GetComponent<IFire>();
        }

        protected override void Start()
        {
            base.Start();
            _settings = BS.Settings.instance.Data.Player.Fire;
            _currentBulletSpeed = _settings.Speed;
        }

        /// <summary>
        /// Обновить состояние скорости стрельбы
        /// true - Увеличенная скорость
        /// false - Стандартная скорость
        /// </summary>
        /// <param name="isSpeedImproved">Состояние</param>
        public void UpdateImprovedState(bool isSpeedImproved)
        {
            _currentBulletSpeed = isSpeedImproved ? _settings.ImprovedSpeed : _settings.Speed;
        }

        /// <summary>
        /// Обновить состояние ведения огня
        /// true - Очередь (два патрона)
        /// false - Одиночные
        /// </summary>
        /// <param name="isTurnOn">Состояние</param>
        public void UpdateTurnState(bool isTurnOn)
        {
            _sameTimeShotCount = isTurnOn ? 2 : 1;
        }

        /// <summary>
        /// Сбрасывает настройки после смерти
        /// </summary>
        public void ResetDirection()
        {
            _lastMoveDirection = Vector2.up;
        }

        protected override void DoFire()
        {
            var canShot = CheckShotOpportunity();

            // Запоминаем направление последнего движения
            if (_dir.Direction != Vector2.zero)
                _lastMoveDirection = _dir.Direction;

            // Если можем стрелять и мы в состоянии огня - спауним и отправляем пулю
            if (canShot && _fire.isFire)
            {
                var position = transform.position + (Vector3)(_fireOffset * _lastMoveDirection);
                var bullet = BulletSpawner.Instance.SpawnBullet(
                    _lastMoveDirection, 
                    _currentBulletSpeed, 
                    position, 
                    Bullet.Owner.Player,
                    BulletType
                );
                _emittedBullets.Enqueue(bullet);
                _delay = _settings.Delay;
                bullet.OnEnemyDestroy += EnemyDestroyHandle;
                bullet.OnBulletDespawn += BulletDespawnHandle;
                OnFire?.Invoke();
            }
        }

        private void EnemyDestroyHandle(Enemy.EnemyType type)
        {
            OnEnemyDestroy?.Invoke(type);
        }

        private void BulletDespawnHandle(Bullet bullet)
        {
            if (bullet != null)
            {
                bullet.OnEnemyDestroy -= EnemyDestroyHandle;
                bullet.OnBulletDespawn -= BulletDespawnHandle;
            }
        }

        #region Player Fire Settings
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Задержка выстрела игрока
            /// </summary>
            public float Delay { get { return delay; } }

            /// <summary>
            /// Скорость выстрела
            /// </summary>
            public float Speed { get { return speed; } }

            /// <summary>
            /// Скорость выстрела после увеличения уровня
            /// </summary>
            public float ImprovedSpeed { get { return improvedSpeed; } }

            [Tooltip("Задержка выстрела игрока")]
            [SerializeField]
            private float delay;

            [Tooltip("Скорость выстрела")]
            [SerializeField]
            private float speed;

            [Tooltip("Скорость выстрела после увеличения уровня")]
            [SerializeField]
            private float improvedSpeed;
        }
        #endregion
    }
}
