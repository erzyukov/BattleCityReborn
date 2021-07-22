using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Базовый компонент стрельбы
    /// </summary>
    [RequireComponent(typeof(IDirection), typeof(BoxCollider2D))]
    public abstract class BaseFireHandler : MonoBehaviour
    {
        protected IDirection _dir;

        protected BoxCollider2D _collider;

        protected float _delay = 0;
        protected Vector2 _lastMoveDirection = Vector2.up;
        protected float _fireOffset;
        protected int _sameTimeShotCount = 1;
        protected Queue<Bullet> _emittedBullets;

        protected virtual void Awake()
        {
            _dir = GetComponent<IDirection>();
            _collider = GetComponent<BoxCollider2D>();
            _emittedBullets = new Queue<Bullet>();
        }

        protected virtual void Start()
        {
            _fireOffset = _collider.bounds.size.x / 2;
        }

        private void Update()
        {
            DoFire();

            // Запоминаем направление последнего движения
            if (_dir.Direction != Vector2.zero)
                _lastMoveDirection = _dir.Direction;
        }

        /// <summary>
        /// Осуществляет выстрел
        /// </summary>
        protected virtual void DoFire() { }

        /// <summary>
        /// Проверяет может ли игрок стрелять.
        /// Игрок может стрелять если снарядов на сцене меньше чем ему разрешено выпустить (_sameTimeShotCount)
        /// </summary>
        /// <returns>Возможность выстрела</returns>
        protected bool CheckShotOpportunity()
        {
            RefreshEmittedBulletsQueue();

            // если есть хоть одна пуля, то для следующей включаем задержку
            if (_emittedBullets.Count != 0 && _delay > 0)
            {
                _delay -= Time.deltaTime;
                return false;
            }

            // если нет наших выпущенных снарядов - можно стрелять
            if (_emittedBullets.Count <= _sameTimeShotCount - 1)
                return true;

            return false;
        }

        /// <summary>
        /// Обновляет содержимое очереди
        /// </summary>
        private void RefreshEmittedBulletsQueue()
        {
            // в зависимости от того, сколько мы можем сделать одновременных выстрелов,
            // столько же раз проверяем выпущенные снаряды
            for (var i = 0; i < _sameTimeShotCount; i++)
            {
                if (_emittedBullets.Count <= 0) return;

                var bullet = _emittedBullets.Peek();
                // если объект снаряда не активен - можно удалить его из очереди
                if (!bullet.IsActive)
                    _emittedBullets.Dequeue();
            }
        }

    }
}
