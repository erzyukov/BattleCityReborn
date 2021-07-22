using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент UI. Для отображение количества оставшихся врагов.
    /// </summary>
    public class UIEnemyCounterView : MonoBehaviour
    {
        [Tooltip("Префаб представления врага")]
        [SerializeField]
        private GameObject prefab;

        private Stack<GameObject> _enemies;

        private void Awake()
        {
            _enemies = new Stack<GameObject>();
        }

        /// <summary>
        /// Инициализирует отображение количества врагов заданным значением
        /// </summary>
        /// <param name="enemyCount">Количество</param>
        public void Init(int enemyCount)
        {
            for (var i = 0; i < enemyCount; i++)
            {
                var enemyView = Instantiate<GameObject>(prefab, transform);
                SetViewPlace(enemyView, i % 2, i / 2);
                _enemies.Push(enemyView);
            }
        }


        /// <summary>
        /// Снижает количество отображаемых врагов на единицу
        /// </summary>
        public void DecreaseEnemyCount()
        {
            if (_enemies != null)
            {
                var enemy = _enemies.Pop();
                enemy.SetActive(false);
            }
        }

        private void SetViewPlace(GameObject enemyView, int x, int y)
        {
            var rectTransform = enemyView.GetComponent<RectTransform>();

            var upperOffset = -(y * 8);
            var rightOffset = (x + 1) * 8;
            var lowerOffset = -((y + 1) * 8);
            var leftOffset = x * 8;

            rectTransform.offsetMax = new Vector2(rightOffset, upperOffset);
            rectTransform.offsetMin = new Vector2(leftOffset, lowerOffset);
        }
    }
}