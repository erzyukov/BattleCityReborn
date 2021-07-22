using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за разрушение Tilemap
    /// </summary>
    [RequireComponent(typeof(Tilemap))]
    public class MapDestruction : MonoBehaviour
    {
        public Action<TileType> OnTileTakeDamage;

        private Tilemap _map;
        private Settings _settings;

        private void Awake()
        {
            _map = GetComponent<Tilemap>();
        }

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Map.Destruction;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet"))
                return;
            
            var bullet = collision.gameObject.GetComponent<Bullet>();
            var direction = bullet.GetDirection();

            var positions = GetGridCollision(collision.contacts, direction * 0.02f);

            for (var i = 0; i < positions.Length; i++)
            {
                if (!_map.HasTile(positions[i]))
                    continue;
                
                if (bullet.Parent == Bullet.Owner.Player)
                    TakeDamageFromPlayer(_map.GetTile(positions[i]));

                if (_map.GetTile(positions[i]) == _settings.ConcreteFull && bullet.BulletType != Bullet.Type.Piercing)
                    continue;

                if (_map.GetTile(positions[i]) == _settings.BrickFull)
                {
                    // если тайл был целым - половиним с необходимой стороны
                    if (direction == Vector2.up)
                        _map.SetTile(positions[i], _settings.BrickUpper);
                    else if (direction == Vector2.down)
                        _map.SetTile(positions[i], _settings.BrickBottom);
                    else if (direction == Vector2.left)
                        _map.SetTile(positions[i], _settings.BrickLeft);
                    else if (direction == Vector2.right)
                        _map.SetTile(positions[i], _settings.BrickRight);
                }
                else
                {
                    // если тайл был не целым - удаляем его
                    // или били бронебойными
                    _map.SetTile(positions[i], null);
                }
            }
        }

        private void TakeDamageFromPlayer(TileBase tileBase)
        {
            if (tileBase == _settings.ConcreteFull)
                OnTileTakeDamage?.Invoke(TileType.Concrete);
            else
                OnTileTakeDamage?.Invoke(TileType.Brick);
        }

        /// <summary>
        /// Возвращает массив уникальных координат тайлов с которыми был контакт
        /// </summary>
        /// <param name="contacts">Массив точек контакта колизии</param>
        /// <param name="contactOffset">Отступ от точки контакта</param>
        /// <returns>Массив координат тайлов</returns>
        private Vector3Int[] GetGridCollision(ContactPoint2D[] contacts, Vector2 contactOffset)
        {
            if (contacts.Length == 0)
                return null;

            var result = new List<Vector3Int>();
            for(var i = 0; i < contacts.Length; i++)
            {
                var gridPosition = _map.WorldToCell(contacts[i].point + contactOffset);
                result.Add(gridPosition);
            }

            return result.Distinct().ToArray();
        }

        public enum TileType { Brick, Concrete };

        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Тайл с полным кирпичем
            /// </summary>
            public TileBase BrickFull { get { return brickFull; } }

            /// <summary>
            /// Тайл с верхним кирпичем
            /// </summary>
            public TileBase BrickUpper { get { return brickUpper; } }

            /// <summary>
            /// Тайл с нижним кирпичем
            /// </summary>
            public TileBase BrickBottom { get { return brickBottom; } }

            /// <summary>
            /// Тайл с левым кирпичем
            /// </summary>
            public TileBase BrickLeft { get { return brickLeft; } }

            /// <summary>
            /// Тайл с правым кирпичем
            /// </summary>
            public TileBase BrickRight { get { return brickRight; } }

            /// <summary>
            /// Тайл с бетоном
            /// </summary>
            public TileBase ConcreteFull { get { return concreteFull; } }

            [Tooltip("Тайл с полным кирпичем")]
            [SerializeField]
            private TileBase brickFull;

            [Tooltip("Тайл с верхним кирпичем")]
            [SerializeField]
            private TileBase brickUpper;

            [Tooltip("Тайл с нижним кирпичем")]
            [SerializeField]
            private TileBase brickBottom;

            [Tooltip("Тайл с левым кирпичем")]
            [SerializeField]
            private TileBase brickLeft;

            [Tooltip("Тайл с правым кирпичем")]
            [SerializeField]
            private TileBase brickRight;

            [Tooltip("Тайл с бетоном")]
            [SerializeField]
            private TileBase concreteFull;
        }

    }
}