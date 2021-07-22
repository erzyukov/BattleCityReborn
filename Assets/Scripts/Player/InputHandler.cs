using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент обработки ввода пользовательских данных
    /// </summary>
    public class InputHandler : MonoBehaviour, IDirection, IFire
    {
        /// <summary>
        /// Текущее направление введенное пользователем
        /// </summary>
        public Vector2 Direction { get; private set; }

        /// <summary>
        /// Нажата ли клавиша выстрела
        /// </summary>
        public bool isFire { get; private set; }

        private InputSettings _settings;

        private void Awake()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange += GameStateChangeHandle;
        }

        private void Update()
        {
            if (GameState.Instance.State == GameState.StateType.Game || GameState.Instance.State == GameState.StateType.Win)
            {
                UpdateMoveDirection();
                UpdateFireState();
            }
        }

        public void Init(InputSettings settings)
        {
            _settings = settings;
        }

        private void UpdateFireState()
        {
            isFire = Input.GetKey(_settings.Fire);
        }

        private void UpdateMoveDirection()
        {
            if (Input.GetKey(_settings.Up))
                Direction = Vector2.up;
            else if (Input.GetKey(_settings.Down))
                Direction = Vector2.down;
            else if (Input.GetKey(_settings.Left))
                Direction = Vector2.left;
            else if (Input.GetKey(_settings.Right))
                Direction = Vector2.right;
            else
                Direction = Vector2.zero;
        }

        private void GameStateChangeHandle(GameState.StateType state)
        {
            if (state == GameState.StateType.Over || state == GameState.StateType.Score)
            {
                isFire = false;
                Direction = Vector2.zero;
            }
        }

        private void OnDestroy()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange -= GameStateChangeHandle;
        }
    }
}