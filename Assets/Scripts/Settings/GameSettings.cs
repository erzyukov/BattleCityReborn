using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Общие настройки игры
    /// </summary>
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 2)]
    public class GameSettings : ScriptableObject
    {
        /// <summary>
        /// Настройки игрока
        /// </summary>
        [Header("Настройки игрока")]
        public PlayerSettings Player;

        /// <summary>
        /// Настройки пули
        /// </summary>
        [Header("Настройки пули")]
        public BulletSettings Bullet;

        /// <summary>
        /// Настройки взрыва
        /// </summary>
        [Header("Настройки взрыва")]
        public ExplodeSettings Explode;

        /// <summary>
        /// Настройки карты
        /// </summary>
        [Header("Настройки карты")]
        public MapSettings Map;

        /// <summary>
        /// Настройки врага
        /// </summary>
        [Header("Настройки врага")]
        public EnemySettings Enemy;

        /// <summary>
        /// Настройки усилений
        /// </summary>
        [Header("Настройки усилений")]
        public PowerUpSettings PowerUp;

        [Serializable]
        public struct PlayerSettings
        {
            /// <summary>
            /// Настройки передвижения
            /// </summary>
            [Tooltip("Настройки передвижения")]
            public PlayerMovementHandler.Settings Movement;

            /// <summary>
            /// Настройки огня
            /// </summary>
            [Tooltip("Настройки огня")]
            public PlayerFireHandler.Settings Fire;

            /// <summary>
            /// Настройки живучести
            /// </summary>
            [Tooltip("Настройки живучести")]
            public PlayerViabilityHandler.Settings Viability;

            /// <summary>
            /// Настройки спавнера
            /// </summary>
            [Tooltip("Настройки спавнера")]
            public PlayerSpawner.Settings Spawner;

            /// <summary>
            /// Настройки звука
            /// </summary>
            [Tooltip("Настройки звука")]
            public PlayerSoundHandler.Settings Sound;
        }

        [Serializable]
        public struct BulletSettings
        {
            /// <summary>
            /// Настройки спавнера
            /// </summary>
            [Tooltip("Настройки спавнера")]
            public BulletSpawner.Settings Spawner;
        }

        [Serializable]
        public struct ExplodeSettings
        {
            /// <summary>
            /// Настройки спавнера
            /// </summary>
            [Tooltip("Настройки спавнера")]
            public ExplodeSpawner.Settings Spawner;
        }

        [Serializable]
        public struct MapSettings
        {
            /// <summary>
            /// Настройки разрушения
            /// </summary>
            [Tooltip("Настройки разрушения")]
            public MapDestruction.Settings Destruction;

            /// <summary>
            /// Настройки звуков
            /// </summary>
            [Tooltip("Настройки звуков")]
            public MapSoundHandler.Settings Sound;
        }

        [Serializable]
        public struct EnemySettings
        {
            /// <summary>
            /// Настройки смены направления
            /// </summary>
            [Tooltip("Настройки смены направления")]
            public EnemyMoveDirection.Settings Direction;

            /// <summary>
            /// Настройки спавнера
            /// </summary>
            [Tooltip("Настройки спавнера")]
            public EnemySpawner.Settings Spawner;
        }

        [Serializable]
        public struct PowerUpSettings
        {
            /// <summary>
            /// Настройки спавнера
            /// </summary>
            [Tooltip("Настройки спавнера")]
            public PowerUpSpawner.Settings Spawner;
        }

    }
}