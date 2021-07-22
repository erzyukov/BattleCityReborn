using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за логику бункера
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class HQStateHandler : MonoBehaviour
    {
        [SerializeField]
        private Sprite ripSprite;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet"))
                return;
            
            if (_renderer.sprite != ripSprite)
            {
                _renderer.sprite = ripSprite;
                ExplodeSpawner.Instance.SpawnExplode(transform.position, true);
                GameState.Instance.SetState(GameState.StateType.Over);
            }
        }
    }
}