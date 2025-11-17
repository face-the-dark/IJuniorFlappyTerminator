using System.Collections;
using Birds;
using UnityEngine;

namespace Shoot.Enemies
{
    public class EnemyShooter : Shooter<Bird>
    {
        private const float InverseScale = -1f;

        [SerializeField] private float _fireRate = 2f;

        private Coroutine _shootCoroutine;

        public void StartShoot()
        {
            StopShootCoroutine();
            _shootCoroutine = StartCoroutine(Shoot());
        }

        public void StopShoot() => 
            StopShootCoroutine();

        private void StopShootCoroutine()
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
                _shootCoroutine = null;
            }
        }

        private IEnumerator Shoot()
        {
            WaitForSeconds wait = new WaitForSeconds(_fireRate);

            while (enabled)
            {
                yield return wait;

                Shoot(ShootPoint.right * InverseScale);
            }
        }
    }
}