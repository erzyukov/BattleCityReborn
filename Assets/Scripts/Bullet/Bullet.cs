using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Общий компонент пули
    /// </summary>
    [RequireComponent(typeof(BulletMoveDirection), typeof(BulletMovementHandler), typeof(BulletExplodeHandler))]
    public class Bullet : MonoBehaviour, IPoolObject
    {
        /// <summary>
        /// Событие возникающее когда пуля уничтожает врага
        /// </summary>
        public Action<Enemy.EnemyType> OnEnemyDestroy;

        /// <summary>
        /// Событие возникающее при изчезновении пули
        /// </summary>
        public Action<Bullet> OnBulletDespawn;

        /// <summary>
        /// Владелец пули
        /// </summary>
        public Owner Parent { get { return _owner; } }

        /// <summary>
        /// Тип пули (обычная/бронебойная)
        /// </summary>
        public Type BulletType { get { return _type; } }

        private BulletMoveDirection _dir;
        private BulletMovementHandler _movement;
        private BulletExplodeHandler _explode;

        private Owner _owner;
        private Type _type;

        /// <summary>
        /// Активность объекта
        /// </summary>
        public bool IsActive { get { return gameObject.activeSelf; } }


        private void Awake()
        {
            _dir = GetComponent<BulletMoveDirection>();
            _movement = GetComponent<BulletMovementHandler>();
            _explode = GetComponent<BulletExplodeHandler>();

            _explode.OnEnemyDestroy += EnemyDestroy;
        }

        /// <summary>
        /// Инициализация объекта при спауне
        /// </summary>
        /// <param name="direction">Направление</param>
        /// <param name="owner">Тип</param>
        /// <param name="layer">Слой</param>
        public void Init(Vector2 direction, float speed, Owner owner, LayerMask layer, Type type)
        {
            _dir.Direction = direction;
            _owner = owner;
            _type = type;
            gameObject.layer = (int) Mathf.Log(layer.value, 2);
            SetSpeed(speed);
        }

        /// <summary>
        /// Возвращает направление движения пули
        /// </summary>
        /// <returns>Направление</returns>
        public Vector2 GetDirection()
        {
            return _dir.Direction;
        }

        /// <summary>
        /// Устанавливает скорость пули
        /// </summary>
        /// <param name="value">Скорость</param>
        public void SetSpeed(float value)
        {
            _movement.Speed = value;
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            OnBulletDespawn?.Invoke(this);
        }

        private void EnemyDestroy(Enemy.EnemyType type)
        {
            OnEnemyDestroy?.Invoke(type);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ReturnToPool();
        }

        private void OnDestroy()
        {
            _explode.OnEnemyDestroy -= EnemyDestroy;
        }

        /// <summary>
        /// Определяет кто выпустил пулю
        /// </summary>
        public enum Owner { Player, Enemy }

        /// <summary>
        /// Определяет тип пули (обычная/бронебойная)
        /// </summary>
        public enum Type { Simple, Piercing }

    }
}