using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Базовый класс передвижения объектов
    /// </summary>
    [RequireComponent(typeof(IDirection), typeof(Rigidbody2D))]
    public abstract class BaseMovementHandler : MonoBehaviour
    {
        /// <summary>
        /// Компонент определяющий направление движение
        /// </summary>
        protected IDirection _dir;

        protected Rigidbody2D _rb;

        protected Vector2 _lastMoveDirection = Vector2.up;

        private void Awake()
        {
            _dir = GetComponent<IDirection>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();

            if (_dir.Direction != Vector2.zero)
                _lastMoveDirection = _dir.Direction;
        }

        /// <summary>
        /// Реализация движения объекты
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// Адаптирует расположение объекта под сетку 
        /// если сменилось направление движения с горизонтального на вертикальное и наоборот
        /// </summary>
        protected void AdaptPositionToGrid()
        {
            var correctedPosition = transform.position;
            // перешли с вертикального на горизонтальное движение
            if (_lastMoveDirection.x == 0 && _dir.Direction.x != 0)
            {
                var y = (float)Math.Round(transform.position.y * 2, MidpointRounding.AwayFromZero) / 2;
                correctedPosition = new Vector2(correctedPosition.x, y);
            }
            // перешли с горизонтального на вертикальное движение
            else if (_lastMoveDirection.y == 0 && _dir.Direction.y != 0)
            {
                var x = (float)Math.Round(transform.position.x * 2, MidpointRounding.AwayFromZero) / 2;
                correctedPosition = new Vector2(x, correctedPosition.y);
            }
            else
            {
                return;
            }
            transform.position = correctedPosition;
        }

    }
}