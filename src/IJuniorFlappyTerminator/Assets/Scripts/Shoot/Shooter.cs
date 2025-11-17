using UnityEngine;

namespace Shoot
{
    public class Shooter<T> : MonoBehaviour where T : IHittable
    {
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected BulletSpawner<T> BulletSpawner;

        public virtual void Reset() => 
            BulletSpawner.Reset();

        protected void Shoot(Vector2 direction)
        {
            Bullet<T> bullet = BulletSpawner.Get();
            bullet.SetPosition(ShootPoint.position);
            bullet.Move(direction);
        }
    }
}