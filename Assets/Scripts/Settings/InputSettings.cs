using UnityEngine;

namespace BS
{
    /// <summary>
    /// Класс для определения схемы ввода данных
    /// </summary>
    [CreateAssetMenu(fileName = "InputSettings", menuName = "Settings/InputSchema", order = 1)]
    public class InputSettings : ScriptableObject
    {
        /// <summary>
        /// Клавиша вверх
        /// </summary>
        public KeyCode Up { get { return up; } }

        /// <summary>
        /// Клавиша вниз
        /// </summary>
        public KeyCode Down { get { return down; } }

        /// <summary>
        /// Клавиша влево
        /// </summary>
        public KeyCode Left { get { return left; } }

        /// <summary>
        /// Клавиша вправо
        /// </summary>
        public KeyCode Right { get { return right; } }

        /// <summary>
        /// Клавиша выстрел
        /// </summary>
        public KeyCode Fire { get { return fire; } }

        [Tooltip("Клавиша вверх")]
        [SerializeField]
        private KeyCode up;

        [Tooltip("Клавиша вниз")]
        [SerializeField]
        private KeyCode down;

        [Tooltip("Клавиша влево")]
        [SerializeField]
        private KeyCode left;

        [Tooltip("Клавиша вправо")]
        [SerializeField]
        private KeyCode right;

        [Tooltip("Клавиша выстрел")]
        [SerializeField]
        private KeyCode fire;
    }
}