using UnityEngine;
using UnityEngine.UI;

namespace BS
{
    /// <summary>
    /// Компонент UI. Для отображение количества жизней игрока.
    /// </summary>
    public class UIPlayerCounterView : MonoBehaviour
    {
        [Tooltip("Текстовый компонент с количеством жизней игрока")]
        [SerializeField]
        private Text playerLifeCount;

        /// <summary>
        /// Обновляет в интерфейсе количество жизней игрока
        /// </summary>
        /// <param name="value">Количество жизней (строка)</param>
        public void SetLifeCount(string value)
        {
            playerLifeCount.text = value;
        }

        /// <summary>
        /// Обновляет в интерфейсе количество жизней игрока
        /// </summary>
        /// <param name="value">Количество жизней (целое)</param>
        public void SetLifeCount(int value)
        {
            if (value < 0) value = 0;
            SetLifeCount(value.ToString());
        }
    }
}