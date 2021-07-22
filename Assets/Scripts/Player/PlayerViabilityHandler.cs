using System;
using System.Collections;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент обрабатывающий состояние жизнеспособности игрока
    /// </summary>
    public class PlayerViabilityHandler : MonoBehaviour, IHealthHolderComponent
    {
        [Tooltip("Эффект бессмертия")]
        [SerializeField]
        private Animator immortalEffect;

        /// <summary>
        /// Оставшиеся жизни
        /// </summary>
        public int RemainLifes { get { return _lifes; } }
        /// <summary>
        /// Событие возникающее при окончании очков здоровья
        /// </summary>
        public event Action OnHealthOver;
        /// <summary>
        /// Событие возникающее при окончании жизней
        /// </summary>
        //public Action OnLifesOver;

        private Settings _settings;

        private int _health;
        private int _lifes;
        private bool _isImmortal;
        private Coroutine _currentImmortalCoroutine;

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Player.Viability;
            _health = _settings.HealthAmount;
            _lifes = _settings.LifesAmount;
        }

        /// <summary>
        /// Активирует бессмертие игрока на заданное время
        /// </summary>
        /// <param name="duration">Длительность бессмертия</param>
        public void ImmortalActivate(float duration)
        {
            if (_currentImmortalCoroutine != null)
                StopCoroutine(_currentImmortalCoroutine);
            SetImmortalityActive(true);
            _currentImmortalCoroutine = StartCoroutine(StopImmortalityCoroutine(duration));
        }

        /// <summary>
        /// Увеличивает количество жизней на единицу
        /// </summary>
        public void IncreaseLifeCount()
        {
            _lifes++;
        }

        private void SetImmortalityActive(bool value)
        {
            if (immortalEffect != null)
                immortalEffect.SetBool("IsImmortal", value);
            _isImmortal = value;
        }

        IEnumerator StopImmortalityCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            SetImmortalityActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet"))
                return;

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.Parent != Bullet.Owner.Enemy || _isImmortal)
                return;
            
            _health--;
            if (_health <= 0)
            {
                _lifes--;
                OnHealthOver?.Invoke();
            }
        }

        #region Player Viability Settings
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Количество хит поинтов
            /// </summary>
            public int HealthAmount { get { return healthAmount; } }

            /// <summary>
            /// Количество жизней
            /// </summary>
            public int LifesAmount { get { return livesAmount; } }

            [Tooltip("Количество хит поинтов")]
            [SerializeField]
            private int healthAmount;

            [Tooltip("Количество жизней")]
            [SerializeField]
            private int livesAmount;
        }
        #endregion
    }
}