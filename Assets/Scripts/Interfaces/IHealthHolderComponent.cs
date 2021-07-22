using System;

namespace BS
{
    /// <summary>
    /// Интерфейс для компонентов имеющих очки жизни
    /// </summary>
    interface IHealthHolderComponent
    {
        event Action OnHealthOver;
    }
}