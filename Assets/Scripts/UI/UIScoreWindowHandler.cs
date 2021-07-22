using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за отображения окна подсчета очков по окончании игры
    /// </summary>
    public class UIScoreWindowHandler : MonoBehaviour
    {
        [Tooltip("UI элемент отображающий итог игры")]
        [SerializeField]
        private UIScoreView scoreView;

        [Tooltip("Спавнер игроков")]
        [SerializeField]
        private PlayerSpawner playerSpawner;

        private void Awake()
        {
            GameState.Instance.OnGameStateChange += GameStateChangeHandle;
        }

        private void GameStateChangeHandle(GameState.StateType state)
        {
            if (state == GameState.StateType.Score)
                foreach(var kvp in playerSpawner.Players)
                    scoreView.ShowPlayerScore(kvp.Key, kvp.Value.Score, kvp.Value.KillCounter);
        }

        private void OnDestroy()
        {
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange -= GameStateChangeHandle;
        }
    }
}