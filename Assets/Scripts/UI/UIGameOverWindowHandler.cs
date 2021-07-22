using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент управления окном конца игры
    /// </summary>
    public class UIGameOverWindowHandler : MonoBehaviour
    {
        [Tooltip("UI элемент отображающий состояние конца игры")]
        [SerializeField]
        private UIGameOverView gameOverView;

        private void Awake()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange += GameStateChangeHandle;
        }

        private void GameStateChangeHandle(GameState.StateType state)
        {
            if (state == GameState.StateType.Over)
                gameOverView.SetGameOverState();
        }

        private void OnDestroy()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange -= GameStateChangeHandle;
        }
    }
}