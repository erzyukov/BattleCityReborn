using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за отображение окна подсчета очков
    /// </summary>
    public class UIScoreView : MonoBehaviour
    {
        [Tooltip("Массив представлений очков игрока")]
        [SerializeField]
        private UIScorePlayerView[] players;

        private Dictionary<Player.NumberType, UIScorePlayerView> _playerViews;
        private Dictionary<Enemy.EnemyType, int> _scorePrice;

        private void Awake()
        {
            gameObject.SetActive(false);
            _playerViews = new Dictionary<Player.NumberType, UIScorePlayerView>();
            _scorePrice = new Dictionary<Enemy.EnemyType, int>();

            for (var i = 0; i < players.Length; i++)
                _playerViews.Add(players[i].PlayerNumber, players[i]);

            var enemies = Settings.instance.Data.Enemy.Spawner.EnemyList;
            for(var i = 0; i < enemies.Length; i++)
                if (!_scorePrice.ContainsKey(enemies[i].Type))
                    _scorePrice.Add(enemies[i].Type, enemies[i].ScoreAmount);
        }

        /// <summary>
        /// Выставляет статистику заданного игрока по заданным данным
        /// </summary>
        /// <param name="number">Номер игрока</param>
        /// <param name="startScore">Очки за бонусы</param>
        /// <param name="stats">Статистика по килам</param>
        public void ShowPlayerScore(Player.NumberType number, int startScore, Dictionary<Enemy.EnemyType, int> stats)
        {
            gameObject.SetActive(true);

            var total = startScore;
            var kills = 0;
            foreach(var kvp in stats)
            {
                var price = _scorePrice.ContainsKey(kvp.Key) ? _scorePrice[kvp.Key] : 0;
                _playerViews[number].SetScoreByEnemyType(kvp.Key, kvp.Value, price);
                total += kvp.Value * price;
                kills += kvp.Value;
            }

            _playerViews[number].SetTotalScore(total);
            _playerViews[number].SetTotalKills(kills);
        }
    }
}