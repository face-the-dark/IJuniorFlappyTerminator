using Interactable;
using Shoot.Enemies;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyShooter))]
    public class Enemy : MonoBehaviour, IInteractable
    {
        private EnemyShooter _shooter;

        public bool IsInPool { get; set; }
        
        private void Awake() => 
            _shooter = GetComponent<EnemyShooter>();

        public void Reset()
        {
            gameObject.SetActive(true);
            
            _shooter.StartShoot();
        }

        public void Release()
        {
            gameObject.SetActive(false);
            
            _shooter.StopShoot();
        }

        public void SetPosition(Vector3 spawnPoint) =>
            transform.position = spawnPoint;
    }
}