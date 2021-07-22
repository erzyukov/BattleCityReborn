using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BS
{
    /// <summary>
    /// Компонент обрабатывающий состояние карты
    /// </summary>
    public class MapStateHandler : MonoBehaviour
    {
        [Tooltip("Компонент Tilemap карты")]
        [SerializeField]
        private Tilemap map;

        private Vector3Int[] _hqMapPoints;
        private MapDestruction.Settings _mapSettings;

        private void Awake()
        {
            // Задавать через параметры имеет смысл, только если бункер будет менять место на карте
            _hqMapPoints = new Vector3Int[8]
            {
                new Vector3Int(0, -11, 0),
                new Vector3Int(0, -12, 0),
                new Vector3Int(0, -13, 0),
                new Vector3Int(-1, -11, 0),
                new Vector3Int(-2, -11, 0),
                new Vector3Int(-3, -11, 0),
                new Vector3Int(-3, -12, 0),
                new Vector3Int(-3, -13, 0)
            };
        }

        private void Start()
        {
            _mapSettings = BS.Settings.instance.Data.Map.Destruction;
        }

        /// <summary>
        /// Чинит бункер. Выставляет бетонные блоки. После заданной задержки меняет их на кирпичные.
        /// </summary>
        /// <param name="duration">Длительность жизни бетонных блоков</param>
        public void RepairHQ(float duration)
        {
            UpdateHQTiles(_mapSettings.ConcreteFull);
            StartCoroutine(ReturnHQToBrickCoroutine(duration));
        }

        /// <summary>
        /// Обновляет тайлы бункера заданными тайлами.
        /// Если на месте тайла пусто и мы пытаемся поставить кирпичь - то ничего не ставим.
        /// </summary>
        /// <param name="tile">Тайл</param>
        private void UpdateHQTiles(TileBase tile)
        {
            for (var i = 0; i < _hqMapPoints.Length; i++)
            {
                if (tile == _mapSettings.BrickFull && !map.HasTile(_hqMapPoints[i]))
                    continue;

                map.SetTile(_hqMapPoints[i], tile);
            }
        }

        IEnumerator ReturnHQToBrickCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            UpdateHQTiles(_mapSettings.BrickFull);
        }
    }
}