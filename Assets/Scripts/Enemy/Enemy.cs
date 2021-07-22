using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Основной компонент врага
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(EnemyMovementHandler), typeof(EnemyFireHandler))]
    [RequireComponent(typeof(EnemyViabilityHandler), typeof(EnemyStunHandler), typeof(EnemyAnimationHandler))]
    public class Enemy : MonoBehaviour, IPoolObject
    {
        public delegate void DestroyAction(Enemy enemy);
        /// <summary>
        /// Событие возникающее при уничтожении противника
        /// </summary>
        public DestroyAction OnDestroyed;

        /// <summary>
        /// Бонусный враг
        /// </summary>
        public bool IsBonusEnemy { get; set; }

        /// <summary>
        /// Тип врага
        /// </summary>
        public EnemyType Type { get; private set; }

        public bool IsDestroyed { get { return _viability.Health <= 0; } }

        private Animator _animator;
        private EnemyMovementHandler _movement;
        private EnemyFireHandler _fire;
        private EnemyViabilityHandler _viability;
        private EnemyStunHandler _stun;
        private EnemyAnimationHandler _animation;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _movement = GetComponent<EnemyMovementHandler>();
            _fire = GetComponent<EnemyFireHandler>();
            _viability = GetComponent<EnemyViabilityHandler>();
            _stun = GetComponent<EnemyStunHandler>();
            _animation = GetComponent<EnemyAnimationHandler>();

            if (_viability != null)
                _viability.OnHealthOver += DestroyHandler;
        }

        /// <summary>
        /// Инициализация врага при спауне
        /// </summary>
        /// <param name="settings"></param>
        public void Init(Vector3 position, EnemySettings settings, bool isBonus)
        {
            transform.position = position;
            _animator.runtimeAnimatorController = settings.Animator;
            _animation.IsBonus = isBonus;
            IsBonusEnemy = isBonus;
            _movement.Speed = settings.Speed;
            _viability.Health = settings.HealthAmount;
            _fire.Init(settings.OnceTimeShot, settings.OnceTimeShotDelay, settings.MinShotDelay, settings.MaxShotDelay, settings.BulletSpeed);
            Type = settings.Type;
        }

        /// <summary>
        /// Станит врага на заданную длительность в секундах
        /// </summary>
        /// <param name="duration">Длительность</param>
        public void Stun(float duration)
        {
            _stun.Stun(duration);
        }

        /// <summary>
        /// Уничтожает танк
        /// </summary>
        public void Destroy()
        {
            ReturnToPool();
            OnDestroyed?.Invoke(this);
        }

        /// <summary>
        /// Взрыв врага вручную
        /// </summary>
        public void ForcedKill()
        {
            _viability.ForcedKill();
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        private void DestroyHandler()
        {
            Destroy();
        }

        /// <summary>
        /// Возможные типы врага
        /// </summary>
        public enum EnemyType { One, Two, Three, Four };
    }
}