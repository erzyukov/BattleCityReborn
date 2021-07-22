using UnityEngine;

namespace BS
{
    /// <summary>
    /// Компонент стрельбы противника
    /// </summary>
    public class EnemyFireHandler : BaseFireHandler
    {
        /// <summary>
        /// Может ли враг стрелять
        /// </summary>
        public bool CanFire { get; set; } = true;

        private float _bulletSpeed;
        private float _minShotDelay;
        private float _maxShotDelay;
        private float _canFireDelay;

        /// <summary>
        /// Инициализация стрельбы противника
        /// </summary>
        /// <param name="sameTimeShotCount">Количество выстрелов за один раз</param>
        /// <param name="delay">Задержка если выстрелами за один раз</param>
        /// <param name="minShotDelay">Минимальная задержка для осуществления выстрела</param>
        /// <param name="maxShotDelay">Максимальная задержка для осуществления выстрела</param>
        public void Init(int sameTimeShotCount, float delay, float minShotDelay, float maxShotDelay, float bulletSpeed)
        {
            _sameTimeShotCount = sameTimeShotCount;
            _delay = delay;
            _minShotDelay = minShotDelay;
            _maxShotDelay = maxShotDelay;
            _bulletSpeed = bulletSpeed;
        }

        protected override void DoFire()
        {
            // _canFireDelay используется для врага (вместо инпута игрока), чтобы враг стрелял в разные промежутки времени

            var canShot = CheckShotOpportunity();

            if (canShot && _canFireDelay < 0 && CanFire)
            {
                var position = transform.position + (Vector3)(_fireOffset * _lastMoveDirection);
                var bullet = BulletSpawner.Instance.SpawnBullet(_lastMoveDirection, _bulletSpeed, position, Bullet.Owner.Enemy);
                _emittedBullets.Enqueue(bullet);

                _canFireDelay = Random.Range(_minShotDelay, _maxShotDelay);
            }
            else
            {
                _canFireDelay -= Time.deltaTime;
            }
        }
    }
}