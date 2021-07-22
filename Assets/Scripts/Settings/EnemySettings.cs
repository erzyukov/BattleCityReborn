using UnityEngine;

namespace BS
{
    /// <summary>
    /// Настройки врага
    /// </summary>
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/NewEnemyType", order = 4)]
    public class EnemySettings : ScriptableObject
    {
        /// <summary>
        /// Тип врага
        /// </summary>
        public Enemy.EnemyType Type { get { return type; } }

        /// <summary>
        /// Скорость передвижения
        /// </summary>
        public float Speed { get { return speed; } }

        /// <summary>
        /// Скорость пули
        /// </summary>
        public float BulletSpeed { get { return bulletSpeed; } }

        /// <summary>
        /// Аниматор противника
        /// </summary>
        public RuntimeAnimatorController Animator { get { return animator; } }
        /// <summary>
        /// Количество одновременных выстрелов
        /// </summary>
        public int OnceTimeShot { get { return onceTimeShot; } }
        /// <summary>
        /// Задержка при нескольких выстрелах
        /// </summary>
        public float OnceTimeShotDelay { get { return onceTimeShotDelay; } }
        /// <summary>
        /// Количество жизней
        /// </summary>
        public int HealthAmount { get { return healthAmount; } }
        /// <summary>
        /// Количество очков выдаваемых за врага
        /// </summary>
        public int ScoreAmount { get { return scoreAmount; } }
        /// <summary>
        /// Минимальная задержка выстрела
        /// </summary>
        public float MinShotDelay { get { return minShotDelay; } }
        /// <summary>
        /// Максимальная задержка выстрела
        /// </summary>
        public float MaxShotDelay { get { return maxShotDelay; } }

        [Tooltip("Тип врага")]
        [SerializeField]
        private Enemy.EnemyType type;

        [Tooltip("Скорость передвижения")]
        [SerializeField]
        private float speed;

        [Tooltip("Скорость пули")]
        [SerializeField]
        private float bulletSpeed;

        [Tooltip("Аниматор противника")]
        [SerializeField]
        private RuntimeAnimatorController animator;

        [Tooltip("Количество одновременных выстрелов")]
        [SerializeField]
        private int onceTimeShot;

        [Tooltip("Задержка при нескольких выстрелах")]
        [SerializeField]
        private float onceTimeShotDelay;

        [Tooltip("Количество жизней")]
        [SerializeField]
        private int healthAmount;

        [Tooltip("Количество очков выдаваемых за врага")]
        [SerializeField]
        private int scoreAmount;

        [Tooltip("Минимальная задержка выстрела")]
        [SerializeField]
        private float minShotDelay;

        [Tooltip("Максимальная задержка выстрела")]
        [SerializeField]
        private float maxShotDelay;
    }
}