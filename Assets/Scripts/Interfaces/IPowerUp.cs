using UnityEngine;

namespace BS
{
    /// <summary>
    /// Интерфейс усиления
    /// </summary>
    public interface IPowerUp
    {
        // Метод срабатывающий на подбор усиления
        public void PickUp(GameObject target);
    }
}