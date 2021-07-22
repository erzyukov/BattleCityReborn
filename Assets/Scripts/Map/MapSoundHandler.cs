using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий звуки разрушения строений
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MapSoundHandler : MonoBehaviour
    {
        [Tooltip("Компонент MapDestruction")]
        [SerializeField]
        private MapDestruction destruction;

        [Tooltip("Границы карты")]
        [SerializeField]
        private BoundaryHitHandler[] boundaries;

        private Settings _settings;
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            if (destruction != null)
                destruction.OnTileTakeDamage += EnvironmentTakeDamageHandle;

            for(var i = 0; i < boundaries.Length; i++)
            {
                if (boundaries == null)
                    continue;
                boundaries[i].OnBoundaryTakeDamage += EnvironmentTakeDamageHandle;
            }
        }

        private void Start()
        {
            _settings = BS.Settings.instance.Data.Map.Sound;
        }

        private void EnvironmentTakeDamageHandle(MapDestruction.TileType type)
        {
            switch (type)
            {
                case MapDestruction.TileType.Brick:
                    _source.PlayOneShot(_settings.Brick);
                    break;
                case MapDestruction.TileType.Concrete:
                    _source.PlayOneShot(_settings.Concrete);
                    break;
            }
        }

        private void OnDestroy()
        {
            if (destruction != null)
                destruction.OnTileTakeDamage -= EnvironmentTakeDamageHandle;

            for (var i = 0; i < boundaries.Length; i++)
            {
                if (boundaries == null)
                    continue;
                boundaries[i].OnBoundaryTakeDamage -= EnvironmentTakeDamageHandle;
            }
        }

        #region Map Sound Settings
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Звук кирпичей
            /// </summary>
            public AudioClip Brick { get { return brick; } }

            /// <summary>
            /// Звук бетона
            /// </summary>
            public AudioClip Concrete { get { return concrete; } }

            [Tooltip("Звук кирпичей")]
            [SerializeField]
            private AudioClip brick;

            [Tooltip("Звук бетона")]
            [SerializeField]
            private AudioClip concrete;
        }
        #endregion

    }
}
