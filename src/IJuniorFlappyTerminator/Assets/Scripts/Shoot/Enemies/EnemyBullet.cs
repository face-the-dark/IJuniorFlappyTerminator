using System;
using Birds;
using Interactable;
using UnityEngine;

namespace Shoot.Enemies
{
    public class EnemyBullet : Bullet, IInteractable
    {
        public event Action<EnemyBullet> BirdHit;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bird bird)) 
                BirdHit?.Invoke(this);
        }
    }
}