using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Shoot
{
    public class BulletSpawner<T> : MonoBehaviour where T : IHittable
    {
        [SerializeField] private Bullet<T> _prefab;
        [SerializeField] private int _poolCapacity = 10;
        [SerializeField] private int _poolMaxSize = 1000;

        private ObjectPool<Bullet<T>> _pool;
        private List<Bullet<T>> _activeBullets;

        public event Action<T> Hit;

        private void Awake()
        {
            _pool = new ObjectPool<Bullet<T>>(
                createFunc: Create,
                actionOnGet: Reset,
                actionOnRelease: Release,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
            
            _activeBullets = new List<Bullet<T>>();
        }

        public Bullet<T> Get()
        {
            Bullet<T> bullet = _pool.Get();
            
            _activeBullets.Add(bullet);
            
            bullet.LifetimeExpired += OnLifetimeExpired;
            bullet.Hit += OnHit;
            
            return bullet;
        }

        public void Reset()
        {
            if (_activeBullets == null)
                return;

            for (int i = _activeBullets.Count - 1; i >= 0; i--) 
                PutIntoPool(_activeBullets[i]);
        }

        private void OnHit(T hittable, Bullet<T> bullet)
        {
            PutIntoPool(bullet);
            
            Hit?.Invoke(hittable);
        }

        private void OnLifetimeExpired(Bullet<T> bullet) => 
            PutIntoPool(bullet);
        
        private void PutIntoPool(Bullet<T> bullet)
        {
            bullet.Hit -= OnHit;
            bullet.LifetimeExpired -= OnLifetimeExpired;
            
            _activeBullets.Remove(bullet);
            _pool.Release(bullet);
        }
        
        private Bullet<T> Create() =>
            Instantiate(_prefab);

        private void Reset(Bullet<T> bullet) => 
            bullet.Reset();

        private void Release(Bullet<T> bullet) => 
            bullet.Release();
    }
}