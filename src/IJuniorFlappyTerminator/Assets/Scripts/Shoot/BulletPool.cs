using UnityEngine;
using UnityEngine.Pool;

namespace Shoot
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;
        [SerializeField] private int _poolCapacity = 5;
        [SerializeField] private int _poolMaxSize = 5;

        private ObjectPool<Bullet> _pool;
        
        private void Awake()
        {
            _pool = new ObjectPool<Bullet>(
                createFunc: Create,
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        public Bullet Get()
        {
            Bullet bullet = _pool.Get();
            
            bullet.LifetimeExpired += Release;
            
            return bullet;
        }

        public void Release(Bullet bullet)
        {
            bullet.LifetimeExpired -= Release;

            if (bullet.IsInPool == false) 
                _pool.Release(bullet);
        }

        private Bullet Create() =>
            Instantiate(_prefab);

        private void ActionOnGet(Bullet bullet)
        {
            bullet.IsInPool = false;
            
            bullet.Reset();
        }

        private void ActionOnRelease(Bullet bullet)
        {
            bullet.IsInPool = true;
            
            bullet.Release();
        }
    }
}