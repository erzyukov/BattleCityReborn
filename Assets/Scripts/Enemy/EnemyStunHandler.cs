using System.Collections;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за стан врага
    /// </summary>
    [RequireComponent(typeof(EnemyMovementHandler), typeof(Animator), typeof(EnemyFireHandler))]
    [RequireComponent(typeof(EnemyMoveDirection))]
    public class EnemyStunHandler : MonoBehaviour
    {
        private EnemyMovementHandler _movement;
        private Animator _animator;
        private EnemyFireHandler _fire;
        private EnemyMoveDirection _dir;

        private float _previousSpeed;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovementHandler>();
            _animator = GetComponent<Animator>();
            _fire = GetComponent<EnemyFireHandler>();
            _dir = GetComponent<EnemyMoveDirection>();
        }

        /// <summary>
        /// Станит врага на заданную длительность в секундах
        /// </summary>
        /// <param name="duration">Длительность</param>
        public void Stun(float duration)
        {
            _previousSpeed = _movement.Speed;
            SetStunState(true);
            StartCoroutine(RemoveStunState(duration));
        }

        private void SetStunState(bool state)
        {
            _movement.Speed = state ? 0 : _previousSpeed;
            _animator.speed = state ? 0 : 1;
            _fire.CanFire = !state;
            _dir.CanMove = !state;
        }

        IEnumerator RemoveStunState(float duration)
        {
            yield return new WaitForSeconds(duration);
            SetStunState(false);
        }
    }
}