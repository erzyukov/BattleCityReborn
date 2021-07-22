using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за звуки игрока
    /// </summary>
    [RequireComponent(typeof(IDirection), typeof(AudioSource), typeof(PlayerFireHandler))]
    public class PlayerSoundHandler : MonoBehaviour
    {
        private Settings _settings;

        private IDirection _dir;
        private PlayerFireHandler _fire;
        private AudioSource _source;

        private Vector2 _lastMoveDirection = Vector2.zero;

        private void Awake()
        {
            _dir = GetComponent<IDirection>();
            _fire = GetComponent<PlayerFireHandler>();
            _source = GetComponent<AudioSource>();
            _fire.OnFire += OnFireHandle;
            GameState.Instance.OnGameStateChange += GameStateChangeHandle;
        }
        
        private void Start()
        {
            _settings = BS.Settings.instance.Data.Player.Sound;
            _source.clip = _settings.Idle;
            _source.Play();
        }

        private void Update()
        {
            if (GameState.Instance.State != GameState.StateType.Game)
                return;

            if (_lastMoveDirection == _dir.Direction)
                return;

            if (_dir.Direction != Vector2.zero)
                PlaySound(_settings.Move);
            else
                PlaySound(_settings.Idle);

            _lastMoveDirection = _dir.Direction;
        }

        private void PlaySound(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }

        private void OnFireHandle()
        {
            _source.PlayOneShot(_settings.Fire);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PowerUp"))
                _source.PlayOneShot(_settings.PowerUp);
        }

        private void GameStateChangeHandle(GameState.StateType state)
        {
            switch (state)
            {
                case GameState.StateType.Pause:
                    _source.Pause();
                    break;
                case GameState.StateType.Game:
                    _source.UnPause();
                    break;
                case GameState.StateType.Win:
                    _source.Stop();
                    break;
                case GameState.StateType.Over:
                    _source.Stop();
                    break;
            }
        }

        private void OnDestroy()
        {
            if (_fire != null)
                _fire.OnFire -= OnFireHandle;
            if (GameState.Instance != null)
                GameState.Instance.OnGameStateChange -= GameStateChangeHandle;
        }

        #region Player Sound Settings
        [Serializable]
        public struct Settings
        {
            /// <summary>
            /// Звук простоя
            /// </summary>
            public AudioClip Idle { get { return idle; } }

            /// <summary>
            /// Звук движения
            /// </summary>
            public AudioClip Move { get { return move; } }

            /// <summary>
            /// Звук выстрела
            /// </summary>
            public AudioClip Fire { get { return fire; } }

            /// <summary>
            /// Звук усиления
            /// </summary>
            public AudioClip PowerUp { get { return powerUp; } }

            [Tooltip("Звук простоя")]
            [SerializeField]
            private AudioClip idle;

            [Tooltip("Звук движения")]
            [SerializeField]
            private AudioClip move;

            [Tooltip("Звук выстрела")]
            [SerializeField]
            private AudioClip fire;

            [Tooltip("Звук усиления")]
            [SerializeField]
            private AudioClip powerUp;
        }
        #endregion
    }
}