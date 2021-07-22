using UnityEngine;
using UnityEngine.SceneManagement;

namespace BS
{
    /// <summary>
    /// Компонент стартующий игру по нажатию клавиши Enter
    /// </summary>
    public class StartGameHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameState.Instance.SetState(GameState.StateType.Game);
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}