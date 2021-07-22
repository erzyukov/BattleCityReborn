using UnityEngine;

namespace BS
{
    /// <summary>
    /// Настройки усиления
    /// </summary>
    [CreateAssetMenu(fileName = "PowerUpSettings", menuName = "Settings/NewPowerUp", order = 5)]
    public class PowerUpSettings : ScriptableObject
    {
        /// <summary>
        /// Тип усиления
        /// </summary>
        public PowerUpItem.Type Type { get { return type; } }

        /// <summary>
        /// Иконка усиления
        /// </summary>
        public Sprite Icon { get { return icon; } }

        /// <summary>
        /// Количество очков за усиление
        /// </summary>
        public int ScoreAmount { get { return scoreAmount; } }

        /// <summary>
        /// Длительность эффекта (не обязательное)
        /// </summary>
        public float Duration { get { return duration; } }

        [Tooltip("Тип усиления")]
        [SerializeField]
        private PowerUpItem.Type type;

        [Tooltip("Иконка усиления")]
        [SerializeField]
        private Sprite icon;

        [Tooltip("Количество очков за усиление")]
        [SerializeField]
        private int scoreAmount;

        [Tooltip("Длительность эффекта (не обязательное)")]
        [SerializeField]
        private float duration;
    }
}