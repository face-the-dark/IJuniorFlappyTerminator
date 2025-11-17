using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace Shoot
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet<T> : MonoBehaviour, IInteractable where T : IHittable
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifetime = 3f;

        private Rigidbody2D _rigidbody2D;

        private Coroutine _lifetimeCoroutine;

        public event Action<Bullet<T>> LifetimeExpired;
        public event Action<T, Bullet<T>> Hit;
        
        private void Awake() =>
            _rigidbody2D = GetComponent<Rigidbody2D>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out T hit)) 
                Hit?.Invoke(hit, this);
        }
        
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
            yield return new WaitForSeconds(_lifetime);
            
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