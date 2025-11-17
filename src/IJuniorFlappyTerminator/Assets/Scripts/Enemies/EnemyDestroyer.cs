using UnityEngine;

namespace Enemies
{
    public class EnemyDestroyer : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                _enemySpawner.ResetShooter(enemy);
                _enemySpawner.PutIntoPool(enemy);
            }
        }
    }
}