using UnityEngine;
using UnityEngine.Pool;

namespace Enemies
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private int _poolCapacity = 5;
        [SerializeField] private int _poolMaxSize = 5;

        private ObjectPool<Enemy> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Enemy>(
                createFunc: Create,
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        public Enemy Get() =>
            _pool.Get();

        public void Release(Enemy enemy)
        {
            if (enemy.IsInPool == false)
                _pool.Release(enemy);
        }

        private Enemy Create() =>
            Instantiate(_prefab);

        private void ActionOnGet(Enemy enemy)
        {
            enemy.IsInPool = false;

            enemy.Reset();
        }

        private void ActionOnRelease(Enemy enemy)
        {
            enemy.IsInPool = true;

            enemy.Release();
        }
    }
}