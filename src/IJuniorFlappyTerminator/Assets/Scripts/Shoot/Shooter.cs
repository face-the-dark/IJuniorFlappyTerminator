using UnityEngine;

namespace Shoot
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected BulletPool BulletPool;
        
        protected Bullet Shoot(Vector2 direction)
        {
            Bullet bullet = BulletPool.Get();
            bullet.SetPosition(ShootPoint.position);
            bullet.Move(direction);
            
            return bullet;
        }
    }
}