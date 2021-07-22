using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за отображение блока подсчета очков определенного игрока
    /// </summary>
    public class UIScorePlayerView : MonoBehaviour
    {
        [Tooltip("Номер игрока")]
        [SerializeField]
        private Player.NumberType playerNumber;

        [Tooltip("Поле общего количества очков игрока")]
        [SerializeField]
        private Text totalScore;

        [Tooltip("Поле общего количества убийств")]
        [SerializeField]
        private Text totalKills;

        [Tooltip("Массив полей очков за противника")]
        [SerializeField]
        private UIScoreEnemyField[] enemies;

        /// <summary>
        /// Номер игрока
        /// </summary>
        public Player.NumberType PlayerNumber { get { return playerNumber; } }

        private Dictionary<Enemy.EnemyType, UIScoreEnemyField> _enemyFields;

        private void Awake()
        {
            gameObject.SetActive(false);
            _enemyFields = new Dictionary<Enemy.EnemyType, UIScoreEnemyField>();
            for(var i = 0; i < enemies.Length; i++)
            {
                _enemyFields.Add(enemies[i].EnemyType, enemies[i]);
                enemies[i].SetPoints(0, 0);
            }
        }

        /// <summary>
        /// Выставляет статистику по заданному типу противника
        /// </summary>
        /// <param name="type">Тип противника</param>
        /// <param name="count">Количиство убитых</param>
        /// <param name="price">Количество очков за одного противника</param>
        public void SetScoreByEnemyType(Enemy.EnemyType type, int count, int price)
        {
            _enemyFields[type].SetPoints(count, price);
        }

        /// <summary>
        /// Выставляет общее количество очков игрока
        /// </summary>
        /// <param name="total">Очки</param>
        public void SetTotalScore(int total)
        {
            gameObject.SetActive(true);
            totalScore.text = total.ToString();
        }

        /// <summary>
        /// Выставляет общее количество убийств
        /// </summary>
        /// <param name="total">Количество убийств</param>
        public void SetTotalKills(int total)
        {
            totalKills.text = total.ToString();
        }
    }
}