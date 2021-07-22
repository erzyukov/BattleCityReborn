using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент обрабатывающий жизнеспособность врага
    /// </summary>
    public class EnemyViabilityHandler : MonoBehaviour, IHealthHolderComponent
    {
        /// <summary>
        /// Событие возникающее при окончании очков здоровья
        /// </summary>
        public event Action OnHealthOver;

        /// <summary>
        /// Количество жизней
        /// </summary>
        public int Health { get; set; }

        public void ForcedKill()
        {
            Health = 0;
            OnHealthOver?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet"))
                return;

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.Parent != Bullet.Owner.Player)
                return;

            Health--;
            if (Health <= 0)
                OnHealthOver?.Invoke();
        }
    }
}