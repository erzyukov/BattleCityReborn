using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент содержащий усиление для игрока
    /// </summary>
    public class PowerUpItem : MonoBehaviour, IPoolObject
    {
        [Tooltip("Компонент SpriteRenderer усиления")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public bool IsActive { get { return gameObject.activeSelf; } }

        private IPowerUp _powerUp;
        private PowerUpSettings _settings;

        public void Init(Vector2 position, PowerUpSettings settings, IPowerUp powerUp)
        {
            transform.position = position;
            spriteRenderer.sprite = settings.Icon;
            _powerUp = powerUp;
            _settings = settings;
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            
            if (_powerUp != null)
                _powerUp.PickUp(collision.gameObject);

            var player = collision.GetComponent<Player>();
            if (player != null)
                player.AddScore(_settings.ScoreAmount);
                
            ReturnToPool();
        }

        public enum Type { Immortal, Destroyer, Upgrade, Repair, Stun, ExtraLife};
    }
}