using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Спавнер пуль
    /// </summary>
    public class BulletSpawner : Singleton<BulletSpawner>
    {
        private Settings _settings;

        private PoolingService<Bullet> _spawner;

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Bullet.Spawner;
            _spawner = new PoolingService<Bullet>(_settings.Prefab, 20, transform, true);
        }

        /// <summary>
        /// Спаунит пулю с заданными настройками
        /// </summary>
        /// <param name="direction">Направление движения</param>
        /// <param name="speed">Скорость</param>
        /// <param name="startPosition">Начальная позиция</param>
        /// <param name="owner">Владелец (кто выпустил пулю)</param>
        /// <param name="type">Тип пули</param>
        /// <returns>Пуля</returns>
        public Bullet SpawnBullet(Vector2 direction, float speed, Vector3 startPosition, Bullet.Owner owner, Bullet.Type type = Bullet.Type.Simple)
        {
            // берем пулю из пула
            var bullet = _spawner.GetFreeElement();
            // ставим пулю в нужное нам место
            bullet.transform.position = startPosition;
            // определяем слой пули
            var layer = _settings.PlayerBulletLayer;
            if (owner == Bullet.Owner.Enemy)
                layer = _settings.EnemyBulletLayer;
            // инициализируем пулю
            bullet.Init(direction, speed, owner, layer, type);
            return bullet;
        }

        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Префаб пули
            /// </summary>
            public Bullet Prefab { get { return prefab; } }
            /// <summary>
            /// Слой для пуль игрока
            /// </summary>
            public LayerMask PlayerBulletLayer { get { return playerBulletLayer; } }
            /// <summary>
            /// Слой для пуль врага
            /// </summary>
            public LayerMask EnemyBulletLayer { get { return enemyBulletLayer; } }

            [Tooltip("Префаб пули")]
            [SerializeField]
            private Bullet prefab;

            [Tooltip("Слой для пуль игрока")]
            [SerializeField]
            private LayerMask playerBulletLayer;

            [Tooltip("Слой для пуль врага")]
            [SerializeField]
            private LayerMask enemyBulletLayer;
        }
    }
}