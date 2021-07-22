using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент содержит направление движения пули
    /// </summary>
    public class BulletMoveDirection : MonoBehaviour, IDirection
    {
        /// <summary>
        /// Направление движения
        /// </summary>
        public Vector2 Direction { get; set; } = Vector2.right;
    }
}