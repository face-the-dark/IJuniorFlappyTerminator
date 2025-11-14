using System.Collections;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyPool))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _repeatRate = 3f;
        [SerializeField] private float _spawnLowerBound = -3.5f;
        [SerializeField] private float _spawnUpperBound = 3.5f;

        private EnemyPool _pool;

        private void Awake() => 
            _pool = GetComponent<EnemyPool>();

        private void Start() => 
            StartCoroutine(DelaySpawn());

        private IEnumerator DelaySpawn()
        {
            WaitForSeconds wait = new WaitForSeconds(_repeatRate);
            
            yield return wait;
            
            StartCoroutine(Spawn(wait));
        }
        
        private IEnumerator Spawn(WaitForSeconds wait)
        {
            while (enabled) 
            {
                float spawnPositionY = Random.Range(_spawnUpperBound, _spawnLowerBound);
                Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
            
                Enemy enemy = _pool.Get();
                enemy.SetPosition(spawnPoint);

                yield return wait;
            }
        }
    }
}