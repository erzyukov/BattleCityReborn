using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Спавнер взрывов
    /// </summary>
    public class ExplodeSpawner : Singleton<ExplodeSpawner>
    {
        private Settings _settings;

        private PoolingService<Explode> _spawner;

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Explode.Spawner;
            _spawner = new PoolingService<Explode>(_settings.Prefab, 10, transform, true);
        }

        public void SpawnExplode(Vector3 startPosition, bool isBigBoom = false)
        {
            var explode = _spawner.GetFreeElement();
            explode.transform.position = startPosition;
            explode.DoBoom(isBigBoom);
        }

        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Префаб взрыва
            /// </summary>
            public Explode Prefab { get { return prefab; } }

            [Tooltip("Префаб взрыва")]
            [SerializeField]
            private Explode prefab;
        }
    }
}
