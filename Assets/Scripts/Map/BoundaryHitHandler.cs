using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент обрабатывающий столкновение пули с краями карты
    /// </summary>
    public class BoundaryHitHandler : MonoBehaviour
    {
        public Action<MapDestruction.TileType> OnBoundaryTakeDamage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet"))
                return;

            var bullet = collision.gameObject.GetComponent<Bullet>();

            if (bullet != null && bullet.Parent == Bullet.Owner.Player)
                OnBoundaryTakeDamage?.Invoke(MapDestruction.TileType.Concrete);
        }
    }
}