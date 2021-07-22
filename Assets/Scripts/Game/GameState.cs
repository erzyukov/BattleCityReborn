using System;
using System.Collections;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за общее состояние игры
    /// </summary>
    public class GameState : Singleton<GameState>
    {
        /// <summary>
        /// Текущее состояние игры
        /// </summary>
        public StateType State { get; private set; }

        /// <summary>
        /// Тип игры по количеству игроков
        /// </summary>
        public GameType CountType { get; set; }

        public Action<StateType> OnGameStateChange;

        private Coroutine scoreStateCoroutine;

        /// <summary>
        /// Устанавливает указанное состояние игры
        /// </summary>
        /// <param name="state">Состояние</param>
        public void SetState(StateType state)
        {
            if (State == state) return;

            State = state;
            OnGameStateChange?.Invoke(state);

            if ((state == StateType.Over || state == StateType.Win) && scoreStateCoroutine == null)
                scoreStateCoroutine = StartCoroutine(SetScoreStateWithDelay(5));
            if (state == StateType.Score)
                scoreStateCoroutine = null;
        }

        /// <summary>
        /// Переходит в состояние подсчета очков после заданной задержки
        /// </summary>
        /// <param name="delay">Задержка</param>
        /// <returns></returns>
        IEnumerator SetScoreStateWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SetState(StateType.Score);
        }

        /// <summary>
        /// Типы игр по количеству игроков
        /// </summary>
        public enum GameType { One, Two };

        /// <summary>
        /// Типы состояния игры
        /// </summary>
        public enum StateType { Menu, Game, Pause, Over, Win, Score }
    }
}