using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Спавнер усилений
    /// </summary>
    public class PowerUpSpawner : MonoBehaviour
    {
        [Tooltip("Ссылка на компонент спаунера врагов")]
        [SerializeField]
        private EnemySpawner enemySpawner;

        [Tooltip("Нижняя левая граница спавна")]
        [SerializeField]
        private Transform bottomLeftEdge;

        [Tooltip("Верхняя правая граница спавна")]
        [SerializeField]
        private Transform topRightEdge;

        [Tooltip("Компонент MapStateHandler уровня")]
        [SerializeField]
        private MapStateHandler map;

        private Settings _settings;
        private PoolingService<PowerUpItem> _spawner;

        private PowerUpItem _current;

        private void Awake()
        {
            if (enemySpawner != null)
                enemySpawner.OnBonusEnemyDestroyed += OnBonusEnemyDestroyed;
        }

        private void Start()
        {
            _settings = BS.Settings.instance.Data.PowerUp.Spawner;
            _spawner = new PoolingService<PowerUpItem>(_settings.Prefab, 2, transform, true);
        }

        /// <summary>
        /// Обрабатывает уничтожение бонусного врага
        /// Добавлет на карту усиление. Если на карте было другое усиление - оно исчезает.
        /// </summary>
        private void OnBonusEnemyDestroyed()
        {
            if (_current != null && _current.IsActive)
                _current.ReturnToPool();

            _current = _spawner.GetFreeElement();
            var settings = GetRandomItemSettings();
            
            _current.Init(GetRandomPosition(), settings, GetPowerUp(settings));
        }

        /// <summary>
        /// Создает и возвращает усиление по заданным настройками
        /// </summary>
        /// <param name="settings">Настройки усиления</param>
        /// <returns>Объект усиления</returns>
        private IPowerUp GetPowerUp(PowerUpSettings settings)
        {
            switch (settings.Type)
            {
                case PowerUpItem.Type.Immortal:
                    return new PowerUpImmortal(settings.Duration);
                case PowerUpItem.Type.Repair:
                    return new PowerUpRepair(map, settings.Duration);
                case PowerUpItem.Type.Upgrade:
                    return new PowerUpUpgrade();
                case PowerUpItem.Type.Stun:
                    return new PowerUpStun(enemySpawner, settings.Duration);
                case PowerUpItem.Type.Destroyer:
                    return new PowerUpDestroyer(enemySpawner);
                case PowerUpItem.Type.ExtraLife:
                    return new PowerUpExtraLife();
            }

            return null;
        }

        /// <summary>
        /// Возвращает настройки случайного усиления
        /// </summary>
        /// <returns></returns>
        private PowerUpSettings GetRandomItemSettings()
        {
            return _settings.PowerUps[UnityEngine.Random.Range(0, _settings.PowerUps.Length)];
        }

        /// <summary>
        /// Возвращает случайную позицию на сцене, ограниченную заданными рамками
        /// позиция округляется до сетки размером в 0.5
        /// </summary>
        /// <returns></returns>
        private Vector2 GetRandomPosition()
        {
            var x = UnityEngine.Random.Range(bottomLeftEdge.position.x, topRightEdge.position.x);
            var y = UnityEngine.Random.Range(bottomLeftEdge.position.y, topRightEdge.position.y);

            x = (float)Math.Round(x * 2, MidpointRounding.AwayFromZero) / 2;
            y = (float)Math.Round(y * 2, MidpointRounding.AwayFromZero) / 2;

            return new Vector2(x, y);
        }

        private void OnDestroy()
        {
            if (enemySpawner != null)
                enemySpawner.OnBonusEnemyDestroyed -= OnBonusEnemyDestroyed;
        }

        #region PowerUp Spawner Settings
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Префаб усиления
            /// </summary>
            public PowerUpItem Prefab { get { return prefab; } }

            /// <summary>
            /// Список усилений которые будут спауниться
            /// </summary>
            public PowerUpSettings[] PowerUps { get { return powerUps; } }

            [Tooltip("Префаб усиления")]
            [SerializeField]
            private PowerUpItem prefab;

            [Tooltip("Список усилений которые будут спауниться")]
            [SerializeField]
            private PowerUpSettings[] powerUps;
        }
        #endregion
    }
}