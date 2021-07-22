namespace BS
{

    /// <summary>
    /// Компонент отвечающий за передвижение пули
    /// </summary>
    public class BulletMovementHandler : BaseMovementHandler
    {
        public float Speed { get; set; }

        protected override void Move()
        {
            var speed = _dir.Direction * Speed;
            _rb.velocity = speed;
        }
    }
}