using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент управляющий объектом взрыва
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class Explode : MonoBehaviour, IPoolObject
    {
        private Animator _animator;
        private AudioSource _source;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _source = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Отобразить взрыв
        /// </summary>
        /// <param name="isBigBoom">Показывать ли большой взрыв</param>
        public void DoBoom(bool isBigBoom)
        {
            _animator.SetTrigger(isBigBoom? "BigBoom": "Boom");
            if (isBigBoom)
                _source.Play();
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}