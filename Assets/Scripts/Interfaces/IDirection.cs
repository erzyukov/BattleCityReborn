using UnityEngine;

namespace BS
{
    /// <summary>
    /// Интерфейс направления движения
    /// </summary>
    public interface IDirection
    {
        /// <summary>
        /// Напроавление
        /// </summary>
        Vector2 Direction { get; }
    }
}