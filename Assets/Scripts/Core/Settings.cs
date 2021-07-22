using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент-синглтон общих настроек игры.
    /// </summary>
    public class Settings : MonoBehaviour
    {
        public static Settings instance = null;

        /// <summary>
        /// Настройки игры
        /// </summary>
        public GameSettings Data { get { return data; } }

        [Tooltip("Настройки игры")]
        [SerializeField]
        private GameSettings data = null;

        private void Awake()
        {
            if (instance == this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            if (data == null)
                throw new Exception("Установите настройки игры в объекте GameBootstrap");
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}