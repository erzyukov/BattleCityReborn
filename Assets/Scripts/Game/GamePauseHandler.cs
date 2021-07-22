using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за паузу в игре
    /// </summary>
    public class GamePauseHandler : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
                return;

            switch(GameState.Instance.State)
            {
                case GameState.StateType.Game:
                    GameState.Instance.SetState(GameState.StateType.Pause);
                    Time.timeScale = 0;
                    break;
                case GameState.StateType.Pause:
                    GameState.Instance.SetState(GameState.StateType.Game);
                    Time.timeScale = 1;
                    break;
            }
        }
    }
}