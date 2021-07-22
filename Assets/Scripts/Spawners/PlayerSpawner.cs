using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Спавнер игроков
    /// </summary>
    public class PlayerSpawner : MonoBehaviour
    {
        [Tooltip("Точка спавна первого игрока")]
        [SerializeField]
        private Transform playerOneSpawnPoint;

        [Tooltip("Точка спавна второго игрока")]
        [SerializeField]
        private Transform playerTwoSpawnPoints;

        /// <summary>
        /// Событие возникающее при смерти игрока. Передает номер игрока.
        /// </summary>
        public Action<Player.NumberType, int> OnPlayerDie;
        /// <summary>
        /// Событие возникающее при увеличении жизни игрока
        /// </summary>
        public Action<Player.NumberType, int> OnPlayerLifeIncrease;
        /// <summary>
        /// Игроки находящиеся на сцене
        /// </summary>
        public Dictionary<Player.NumberType, Player> Players { get { return _players; } }

        private Settings _settings;

        private Dictionary<Player.NumberType, Player> _players;

        private void Awake()
        {
            _players = new Dictionary<Player.NumberType, Player>();
        }

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Player.Spawner;

            // TODO: Вынести инициализацию игроков в отдельный унифицированный метод

            var playerOne = GameObject.Instantiate<Player>(_settings.Prefab, playerOneSpawnPoint.position, Quaternion.identity, transform);
            playerOne.Init(_settings.PlayerOneSettings, _settings.SpawnImmortalDuration, _settings.PlayerOneInputSettings);
            playerOne.OnPlayerDie += PlayerDieHandle;
            playerOne.OnPlayerLifeIncrease += PlayerLifeIncreaseHandle;

            _players.Add(Player.NumberType.One, playerOne);

            if (GameState.Instance.CountType == GameState.GameType.Two)
            {
                var playerTwo = GameObject.Instantiate<Player>(_settings.Prefab, playerTwoSpawnPoints.position, Quaternion.identity, transform);
                playerTwo.Init(_settings.PlayerTwoSettings, _settings.SpawnImmortalDuration, _settings.PlayerTwoInputSettings);
                playerTwo.OnPlayerDie += PlayerDieHandle;
                playerTwo.OnPlayerLifeIncrease += PlayerLifeIncreaseHandle;

                _players.Add(Player.NumberType.Two, playerTwo);
            }
        }

        private void PlayerLifeIncreaseHandle(Player player)
        {
            OnPlayerLifeIncrease?.Invoke(player.Number, player.RemainLifes);
        }

        private void PlayerDieHandle(Player player)
        {
            OnPlayerDie?.Invoke(player.Number, player.RemainLifes);
            if (player.RemainLifes > 0)
                StartCoroutine(PlayerReviveCoroutine(player, _settings.SpawnDelay));
            else
                CheckGameOver(player.Number);
        }

        private void CheckGameOver(Player.NumberType number)
        {
            switch (GameState.Instance.CountType)
            {
                case GameState.GameType.One:
                    if (_players[Player.NumberType.One].RemainLifes <= 0)
                        GameState.Instance.SetState(GameState.StateType.Over);
                    break;
                case GameState.GameType.Two:
                    if (_players[Player.NumberType.One].RemainLifes <= 0 && _players[Player.NumberType.Two].RemainLifes <= 0)
                        GameState.Instance.SetState(GameState.StateType.Over);
                    break;
            }
        }

        IEnumerator PlayerReviveCoroutine(Player player, float delay)
        {
            yield return new WaitForSeconds(delay);
            player.Revive();
        }

        private void OnDestroy()
        {
            foreach(KeyValuePair<Player.NumberType, Player> player in _players)
            {
                if (player.Value != null)
                {
                    player.Value.OnPlayerDie -= PlayerDieHandle;
                    player.Value.OnPlayerLifeIncrease -= PlayerLifeIncreaseHandle;
                }
            }
        }

        #region Player Spawner Settings
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Префаб игрока
            /// </summary>
            public Player Prefab { get { return prefab; } }

            /// <summary>
            /// Задержка перед спауном после смерти
            /// </summary>
            public float SpawnDelay { get { return spawnDelay; } }

            /// <summary>
            /// Длительность бессмертия при спауне
            /// </summary>
            public float SpawnImmortalDuration { get { return spawnImmortalDuration; } }

            /// <summary>
            /// Настройки первого игрока
            /// </summary>
            public PlayerSettings PlayerOneSettings { get { return playerOneSettings; } }

            /// <summary>
            /// Настройки второго игрока
            /// </summary>
            public PlayerSettings PlayerTwoSettings { get { return playerTwoSettings; } }

            /// <summary>
            /// Схема пользовательского ввода первого игрока
            /// </summary>
            public InputSettings PlayerOneInputSettings { get { return playerOneInputSettings; } }

            /// <summary>
            /// Схема пользовательского ввода второго игрока
            /// </summary>
            public InputSettings PlayerTwoInputSettings { get { return playerTwoInputSettings; } }

            [Tooltip("Префаб игрока")]
            [SerializeField]
            private Player prefab;

            [Tooltip("Задержка перед спауном после смерти")]
            [SerializeField]
            private float spawnDelay;

            [Tooltip("Длительность бессмертия при спауне")]
            [SerializeField]
            private float spawnImmortalDuration;

            [Tooltip("Настройки первого игрока")]
            [SerializeField]
            private PlayerSettings playerOneSettings;

            [Tooltip("Настройки второго игрока")]
            [SerializeField]
            private PlayerSettings playerTwoSettings;

            [Tooltip("Схема пользовательского ввода первого игрока")]
            [SerializeField]
            private InputSettings playerOneInputSettings;

            [Tooltip("Схема пользовательского ввода второго игрока")]
            [SerializeField]
            private InputSettings playerTwoInputSettings;
        }
        #endregion
    }
}
