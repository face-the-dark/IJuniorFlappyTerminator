using Birds;
using Enemies;
using UnityEngine;

namespace Shoot.Birds
{
    [RequireComponent(typeof(BirdInputHandler))]
    [RequireComponent(typeof(ScoreCounter))]
    public class BirdShooter : Shooter
    {
        [SerializeField] private EnemyPool _enemyPool;
        
        private BirdInputHandler _birdInputHandler;
        private ScoreCounter _scoreCounter;
        
        private void Awake()
        {
            _birdInputHandler = GetComponent<BirdInputHandler>();
            _scoreCounter = GetComponent<ScoreCounter>();
        }

        private void OnEnable() => 
            _birdInputHandler.Shot += OnShot;

        private void OnDisable() => 
            _birdInputHandler.Shot -= OnShot;

        private void OnShot()
        {
            Bullet bullet = Shoot(ShootPoint.right);

            if (bullet is BirdBullet birdBullet) 
                birdBullet.EnemyHit += OnEnemyHit;
        }

        private void OnEnemyHit(Enemy enemy, BirdBullet birdBullet)
        {
            _scoreCounter.Add();
            _enemyPool.Release(enemy);
            
            birdBullet.EnemyHit -= OnEnemyHit;
            BulletPool.Release(birdBullet);
        }
    }
}