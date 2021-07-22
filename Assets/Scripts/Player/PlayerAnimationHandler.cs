using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за анимацию передвижения игрока
    /// </summary>
    public class PlayerAnimationHandler : BaseAnimationHandler
    {
        protected override void AnimationHandle()
        {
            if (_dir.Direction != Vector2.zero)
            {
                _animator.SetFloat("Horizontal", _dir.Direction.x);
                _animator.SetFloat("Vertical", _dir.Direction.y);
                _animator.SetBool("IsMoving", true);
            }
            else
            {
                _animator.SetBool("IsMoving", false);
            }
        }
    }
}