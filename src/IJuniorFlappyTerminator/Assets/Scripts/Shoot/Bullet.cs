using System;
using System.Collections;
using UnityEngine;

namespace Shoot
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifetime = 3f;

        private Rigidbody2D _rigidbody2D;

        private Coroutine _lifetimeCoroutine;

        public event Action<Bullet> LifetimeExpired;

        public bool IsInPool { get; set; }
        
        private void Awake() =>
            _rigidbody2D = GetComponent<Rigidbody2D>();
        
        public void SetPosition(Vector2 shootPointPosition) => 
            transform.position = shootPointPosition;

        public void Move(Vector2 direction) => 
            _rigidbody2D.AddForce(direction * _speed, ForceMode2D.Impulse);

        public void Reset()
        {
            gameObject.SetActive(true);
            
            _lifetimeCoroutine = StartCoroutine(DecreaseLifetime());
        }

        public void Release()
        {
            gameObject.SetActive(false);
            
            StopLifetimeCoroutine();
        }

        private IEnumerator DecreaseLifetime()
        {
            float currentLifeTime = _lifetime;

            while (currentLifeTime >= 0)
            {
                currentLifeTime -= Time.deltaTime;
                
                yield return null;
            }
            
            LifetimeExpired?.Invoke(this);
        }

        private void StopLifetimeCoroutine()
        {
            if (_lifetimeCoroutine != null)
            {
                StopCoroutine(_lifetimeCoroutine);
                _lifetimeCoroutine = null;
            }
        }
    }
}