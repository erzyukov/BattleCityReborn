using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент управления пользовательским интерфейсом
    /// </summary>
    public class UIHUDHandler : MonoBehaviour
    {
        [Tooltip("UI элемент отображающий количество жизни первого игрока")]
        [SerializeField]
        private UIPlayerCounterView playerOneCounterView;
        [Tooltip("UI элемент отображающий количество жизни первого игрока")]
        [SerializeField]
        private UIPlayerCounterView playerTwoCounterView;
        [Tooltip("UI элемент отображающий количество оставшихся противников")]
        [SerializeField]
        private UIEnemyCounterView enemyCounterView;

        [Tooltip("Спавнер игроков")]
        [SerializeField]
        private PlayerSpawner playerSpawner;
        [Tooltip("Спавнер врагов")]
        [SerializeField]
        private EnemySpawner enemySpawner;

        private void Awake()
        {
            if (playerSpawner != null)
            {
                playerSpawner.OnPlayerDie += OnPlayerLifeCountChange;
                playerSpawner.OnPlayerLifeIncrease += OnPlayerLifeCountChange;
            }
            if (enemySpawner != null)
                enemySpawner.OnEnemySpawn += EnemySpawnHandle;
        }

        private void Start()
        {
            var enemyCount = BS.Settings.instance.Data.Enemy.Spawner.EnemyList.Length;
            var playerLifeCount = BS.Settings.instance.Data.Player.Viability.LifesAmount;

            enemyCounterView.Init(enemyCount);
            playerOneCounterView.SetLifeCount(playerLifeCount - 1);
            playerTwoCounterView.SetLifeCount(playerLifeCount - 1);
            if (GameState.Instance.CountType == GameState.GameType.One)
                playerTwoCounterView.gameObject.SetActive(false);
        }

        private void OnPlayerLifeCountChange(Player.NumberType number, int lifesRemain)
        {
            switch (number)
            {
                case Player.NumberType.One:
                    playerOneCounterView.SetLifeCount(lifesRemain - 1);
                    break;
                case Player.NumberType.Two:
                    playerTwoCounterView.SetLifeCount(lifesRemain - 1);
                    break;
            }
        }

        private void EnemySpawnHandle()
        {
            enemyCounterView.DecreaseEnemyCount();
        }

        private void OnDestroy()
        {
            if (playerSpawner != null)
            {
                playerSpawner.OnPlayerDie -= OnPlayerLifeCountChange;
                playerSpawner.OnPlayerLifeIncrease -= OnPlayerLifeCountChange;
            }
            if (enemySpawner != null)
                enemySpawner.OnEnemySpawn -= EnemySpawnHandle;
        }
    }
}