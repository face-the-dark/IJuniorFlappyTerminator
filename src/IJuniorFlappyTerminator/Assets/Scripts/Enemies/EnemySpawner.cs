using System.Collections;
using System.Collections.Generic;
using Shoot.Enemies;
using UnityEngine;
using UnityEngine.Pool;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private int _poolCapacity = 10;
        [SerializeField] private int _poolMaxSize = 1000;

        [SerializeField] private float _repeatRate = 3f;
        [SerializeField] private float _spawnLowerBound = -3.5f;
        [SerializeField] private float _spawnUpperBound = 3.5f;

        private ObjectPool<Enemy> _pool;
        private List<Enemy> _activeEnemies;

        private void Awake()
        {
            _pool = new ObjectPool<Enemy>(
                createFunc: Create,
                actionOnGet: Reset,
                actionOnRelease: Release,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );

            _activeEnemies = new List<Enemy>();
        }

        private void Start() =>
            StartCoroutine(Spawn());

        public void Reset()
        {
            if (_activeEnemies == null)
                return;

            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                ResetShooter(_activeEnemies[i]);
                PutIntoPool(_activeEnemies[i]);
            }
        }

        public void ResetShooter(Enemy enemy)
        {
            if (enemy.TryGetComponent(out EnemyShooter enemyShooter)) 
                enemyShooter.Reset();
        }

        public void PutIntoPool(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
            _pool.Release(enemy);
        }

        private IEnumerator Spawn()
        {
            WaitForSeconds wait = new WaitForSeconds(_repeatRate);

            while (enabled)
            {
                yield return wait;

                float spawnPositionY = Random.Range(_spawnUpperBound, _spawnLowerBound);
                Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

                Enemy enemy = _pool.Get();
                _activeEnemies.Add(enemy);
                enemy.SetPosition(spawnPoint);
            }
        }

        private Enemy Create() => 
            Instantiate(_prefab);

        private void Reset(Enemy enemy) => 
            enemy.Reset();

        private void Release(Enemy enemy) => 
            enemy.Release();
    }
}