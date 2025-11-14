using System;
using Enemies;
using UnityEngine;

namespace Shoot.Birds
{
    public class BirdBullet : Bullet
    {
        public event Action<Enemy, BirdBullet> EnemyHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy)) 
                EnemyHit?.Invoke(enemy, this);
        }
    }
}