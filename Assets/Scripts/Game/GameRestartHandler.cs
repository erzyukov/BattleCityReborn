using UnityEngine;
using UnityEngine.SceneManagement;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за рестарт игры
    /// </summary>
    public class GameRestartHandler : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R))
                return;

            if (GameState.Instance.State == GameState.StateType.Score)
            {
                GameState.Instance.SetState(GameState.StateType.Menu);
                SceneManager.LoadScene("MenuScene");
            }
        }
    }
}