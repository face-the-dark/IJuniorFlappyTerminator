using Enemies;
using UnityEngine;

namespace Shoot.Birds
{
    [RequireComponent(typeof(ScoreCounter))]
    public class BirdShooter : Shooter<Enemy>
    {
        [SerializeField] private EnemySpawner _enemySpawner;

        private ScoreCounter _scoreCounter;

        private void Awake() =>
            _scoreCounter = GetComponent<ScoreCounter>();

        private void OnEnable() => 
            BulletSpawner.Hit += OnHit;

        private void OnDisable() => 
            BulletSpawner.Hit -= OnHit;

        public void Shoot() =>
            Shoot(ShootPoint.right);

        public override void Reset()
        {
            base.Reset();

            _scoreCounter.Reset();
        }

        private void OnHit(Enemy enemy)
        {
            _scoreCounter.Add();
            _enemySpawner.PutIntoPool(enemy);
        }
    }
}