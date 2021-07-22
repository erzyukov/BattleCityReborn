using System;
using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент отвечающий за смену уровней игрока
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(PlayerFireHandler), typeof(PlayerViabilityHandler))]
    public class PlayerGradeHandler : MonoBehaviour
    {
        private RuntimeAnimatorController[] _gradeAnimators;
        private Animator _animator;
        private PlayerFireHandler _fire;
        private PlayerViabilityHandler _viability;

        private int _currentLevel = 0;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _fire = GetComponent<PlayerFireHandler>();
            _viability = GetComponent<PlayerViabilityHandler>();
            _viability.OnHealthOver += PlayerDieHandle;
        }

        /// <summary>
        /// Инициализация компоненты уровня игрока
        /// </summary>
        /// <param name="gradeAnimators">Массив аниматоров для каждого уровня</param>
        public void Init(RuntimeAnimatorController[] gradeAnimators)
        {
            _gradeAnimators = gradeAnimators;
            SetAnimator(_currentLevel);
        }

        /// <summary>
        /// Повышение игрока на один уровень
        /// </summary>
        public void Upgrade()
        {
            _currentLevel++;
            Upgrade(_currentLevel);
        }

        /// <summary>
        /// Установка игроку указанного уровня
        /// </summary>
        /// <param name="level">Уровень</param>
        private void Upgrade(int level)
        {
            if (level >= _gradeAnimators.Length)
                return;

            switch (level)
            {
                case 0:
                    // сбрасываем все бонусы
                    _fire.UpdateImprovedState(false);
                    _fire.UpdateTurnState(false);
                    _fire.BulletType = Bullet.Type.Simple;
                    break;
                case 1:
                    // Увеличиваем скорость пули
                    _fire.UpdateImprovedState(true);
                    break;
                case 2:
                    // Стреляем очередью
                    _fire.UpdateTurnState(true);
                    break;
                case 3:
                    // Пробиваем бетон
                    _fire.BulletType = Bullet.Type.Piercing;
                    break;
            }

            SetAnimator(level);
        }

        /// <summary>
        /// Сбрасываем уровень игрока
        /// </summary>
        public void LevelReset()
        {
            _currentLevel = 0;
            Upgrade(0);
        }

        private void SetAnimator(int value)
        {
            if (value >= _gradeAnimators.Length)
                return;

            _animator.runtimeAnimatorController = _gradeAnimators[value];
        }

        private void PlayerDieHandle()
        {
            LevelReset();
        }

        private void OnDestroy()
        {
            _viability.OnHealthOver -= PlayerDieHandle;
        }
    }
}