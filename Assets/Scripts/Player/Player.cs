using System;
using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Общий компонент для игрока
    /// </summary>
    [RequireComponent(typeof(PlayerViabilityHandler), typeof(PlayerGradeHandler), typeof(InputHandler))]
    [RequireComponent(typeof(PlayerScoreHandler), typeof(PlayerFireHandler))]
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// Оставшиеся жизни игрока
        /// </summary>
        public int RemainLifes { get { return _viability.RemainLifes; } }
        /// <summary>
        /// Номер игрока
        /// </summary>
        public NumberType Number { get { return _number; } }
        /// <summary>
        /// Количество набранных очков
        /// </summary>
        public int Score { get { return _score.Score; } }
        /// <summary>
        /// Счетчик убийств
        /// </summary>
        public Dictionary<Enemy.EnemyType, int> KillCounter { get { return _score.KillCounter; } }
        /// <summary>
        /// Событие вызывается при смерти игрока
        /// </summary>
        public Action<Player> OnPlayerDie;
        /// <summary>
        /// Событие вызывается когда увеличиваются жизни игрока
        /// </summary>
        public Action<Player> OnPlayerLifeIncrease;

        private PlayerViabilityHandler _viability;
        private PlayerGradeHandler _grade;
        private InputHandler _input;
        private PlayerScoreHandler _score;
        private PlayerFireHandler _fire;

        private NumberType _number;
        private Vector3 _startPosition;
        private float _spawnImmortalDuration;

        private void Awake()
        {
            _viability = GetComponent<PlayerViabilityHandler>();
            _grade = GetComponent<PlayerGradeHandler>();
            _input = GetComponent<InputHandler>();
            _score = GetComponent<PlayerScoreHandler>();
            _fire = GetComponent<PlayerFireHandler>();
            _viability.OnHealthOver += HealthOverHandle;
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        /// <summary>
        /// Инициализация параметров игрока
        /// </summary>
        /// <param name="settings">Настройки игрока</param>
        /// <param name="spawnImmortalDuration">Длительность неуязвимости при спавне</param>
        public void Init(PlayerSettings settings, float spawnImmortalDuration, InputSettings inputSettings)
        {
            _number = settings.Number;
            _grade.Init(settings.Animator);
            _input.Init(inputSettings);
            _spawnImmortalDuration = spawnImmortalDuration;
            SetPlayerImmortal(spawnImmortalDuration);
        }

        /// <summary>
        /// Устанавливает бессмертие игрока на заданное количество секунд
        /// </summary>
        /// <param name="duration">Длительность</param>
        public void SetPlayerImmortal(float duration)
        {
            _viability.ImmortalActivate(duration);
        }

        /// <summary>
        /// Устанавливает следующий уровень для игрока
        /// </summary>
        public void SetNextLevel()
        {
            _grade.Upgrade();
        }

        /// <summary>
        /// Добавляет игроку одну жизнь
        /// </summary>
        public void AddExtraLife()
        {
            _viability.IncreaseLifeCount();
            OnPlayerLifeIncrease?.Invoke(this);
        }

        /// <summary>
        /// Восстанавливает игрока на стартовой позиции
        /// </summary>
        public void Revive()
        {
            transform.position = _startPosition;
            gameObject.SetActive(true);
            _fire.ResetDirection();
            SetPlayerImmortal(_spawnImmortalDuration);
        }

        /// <summary>
        /// Добавляет игроку заданное количество очков
        /// </summary>
        /// <param name="amount">Количество очков</param>
        public void AddScore(int amount)
        {
            _score.AddScore(amount);
        }

        private void HealthOverHandle()
        {
            gameObject.SetActive(false);
            OnPlayerDie?.Invoke(this);
        }

        private void OnDestroy()
        {
            _viability.OnHealthOver -= HealthOverHandle;
        }

        /// <summary>
        /// Номер игрока
        /// </summary>
        public enum NumberType { One, Two};
    }
}