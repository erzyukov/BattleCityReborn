using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отображает эффект взрыва, при уничтожении врага.
    /// </summary>
    [RequireComponent(typeof(IHealthHolderComponent))]
    public class ExplodeHandler : MonoBehaviour
    {
        private IHealthHolderComponent _viability;

        private void Awake()
        {
            _viability = GetComponent<IHealthHolderComponent>();
            if (_viability != null)
                _viability.OnHealthOver += DestroyHandler;
        }

        private void DestroyHandler()
        {
            ExplodeSpawner.Instance.SpawnExplode(transform.position, true);
        }

        private void OnDestroy()
        {
            if (_viability != null)
                _viability.OnHealthOver -= DestroyHandler;
        }
    }
}