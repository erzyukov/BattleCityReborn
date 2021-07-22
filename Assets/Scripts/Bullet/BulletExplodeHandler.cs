using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент запускает взрыв при контакте со сторонними объектами
    /// </summary>
    public class BulletExplodeHandler : MonoBehaviour
    {
        /// <summary>
        /// Событие вызываемое при попадании во врага
        /// </summary>
        public Action<Enemy.EnemyType> OnEnemyDestroy;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall") || 
                collision.gameObject.CompareTag("Boundary") || 
                collision.gameObject.CompareTag("Enemy") ||
                collision.gameObject.CompareTag("Player"))
            {
                ExplodeSpawner.Instance.SpawnExplode(transform.position);

                var enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null && enemy.IsDestroyed)
                    OnEnemyDestroy?.Invoke(enemy.Type);
            }
        }
    }
}