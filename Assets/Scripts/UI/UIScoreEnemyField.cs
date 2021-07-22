using UnityEngine;
using UnityEngine.UI;

namespace BS
{
    public class UIScoreEnemyField : MonoBehaviour
    {
        [Tooltip("Тип противника")]
        [SerializeField]
        private Enemy.EnemyType enemyType;
        [Tooltip("Поле количества убитых противников определенного типа")]
        [SerializeField]
        private Text enemyCountField;
        [Tooltip("Поле количества очков за противника определенного типа")]
        [SerializeField]
        private Text enemyScoreField;

        /// <summary>
        /// Тип противника
        /// </summary>
        public Enemy.EnemyType EnemyType { get { return enemyType; } }

        /// <summary>
        /// Выставляет количество убитых противников определенного типа и общее количество очков 
        /// </summary>
        /// <param name="count">Количество противников</param>
        /// <param name="price">Количество очков за одного противника</param>
        public void SetPoints(int count, int price)
        {
            enemyCountField.text = count.ToString();
            enemyScoreField.text = (count * price).ToString();
        }
    }
}