using UnityEngine;

namespace BS
{
    /// <summary>
    /// Настройки игрока
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/NewPlayerType", order = 6)]
    public class PlayerSettings : ScriptableObject
    {
        /// <summary>
        /// Номер игрока
        /// </summary>
        public Player.NumberType Number { get { return number; } }
        /// <summary>
        /// Аниматор игрока по уровню
        /// </summary>
        public RuntimeAnimatorController[] Animator { get { return animator; } }

        [Tooltip("Номер игрока")]
        [SerializeField]
        private Player.NumberType number;

        [Tooltip("Аниматор игрока по уровню")]
        [SerializeField]
        private RuntimeAnimatorController[] animator;
    }
}