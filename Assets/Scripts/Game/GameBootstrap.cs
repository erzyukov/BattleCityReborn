using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент стартующий игру
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            GameState.Instance.SetState(GameState.StateType.Menu);
            // инициализация спавнеров
            var bullet = BulletSpawner.Instance;
            var explode = ExplodeSpawner.Instance;
        }
    }
}