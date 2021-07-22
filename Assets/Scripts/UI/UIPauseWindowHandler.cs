using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент управления окном паузы
    /// </summary>
    public class UIPauseWindowHandler : MonoBehaviour
    {
        [Tooltip("UI элемент отображающий состояние паузы")]
        [SerializeField]
        private UIPauseView pauseView;

        private void Awake()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange += GameStateChangeHandle;
        }

        private void GameStateChangeHandle(GameState.StateType state)
        {
            switch (state)
            {
                case GameState.StateType.Pause:
                    pauseView.SetPauseState(true);
                    break;
                case GameState.StateType.Game:
                    pauseView.SetPauseState(false);
                    break;
            }
        }

        private void OnDestroy()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange -= GameStateChangeHandle;
        }
    }
}