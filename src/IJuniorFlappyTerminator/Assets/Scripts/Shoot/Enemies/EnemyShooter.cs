using System.Collections;
using UnityEngine;

namespace Shoot.Enemies
{
    public class EnemyShooter : Shooter
    {
        private const float InverseScale = -1f;
        
        [SerializeField] private float _fireRate = 2f;

        private Coroutine _shootCoroutine;
        private Coroutine _delayCoroutine;

        public void StartShoot()
        {
            StopDelayCoroutine();
            _delayCoroutine = StartCoroutine(DelayFire());
        }

        public void StopShoot()
        {
            StopDelayCoroutine();
            StopShootCoroutine();
        }

        private IEnumerator DelayFire()
        {
            StopShootCoroutine();

            WaitForSeconds wait = new WaitForSeconds(_fireRate);
            
            yield return wait;
            
            _shootCoroutine = StartCoroutine(Fire(wait));
        }

        private IEnumerator Fire(WaitForSeconds wait)
        {
            while (enabled)
            {
                Bullet bullet = Shoot(ShootPoint.right * InverseScale);

                if (bullet is EnemyBullet enemyBullet) 
                    enemyBullet.BirdHit += OnBirdHit;

                yield return wait;
            }
        }

        private void OnBirdHit(EnemyBullet enemyBullet)
        {
            enemyBullet.BirdHit -= OnBirdHit;
            
            BulletPool.Release(enemyBullet);
        }

        private void StopShootCoroutine()
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
                _shootCoroutine = null;
            }
        }
        
        private void StopDelayCoroutine()
        {
            if (_delayCoroutine != null)
            {
                StopCoroutine(_delayCoroutine);
                _delayCoroutine = null;
            }
        }
    }
}