namespace BS
{
    // Компонент управления анимацией противника
    public class EnemyAnimationHandler : BaseAnimationHandler
    {
        public bool IsBonus = false;

        protected override void AnimationHandle()
        {
            _animator.SetBool("IsBonus", IsBonus);
            base.AnimationHandle();
        }

    }
}