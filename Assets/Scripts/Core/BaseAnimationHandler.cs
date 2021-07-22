using UnityEngine;

namespace BS
{
    // Базовый компонент управления анимацией
    public abstract class BaseAnimationHandler : MonoBehaviour
    {
        /// <summary>
        /// Компонент определяющий направление движение
        /// </summary>
        protected IDirection _dir;
        /// <summary>
        /// Аниматор
        /// </summary>
        protected Animator _animator;

        private void Awake()
        {
            _dir = GetComponent<IDirection>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            AnimationHandle();
        }

        protected virtual void AnimationHandle()
        {
            _animator.SetFloat("Horizontal", _dir.Direction.x);
            _animator.SetFloat("Vertical", _dir.Direction.y);
        }
    }
}