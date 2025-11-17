using System;
using Enemies;
using Shoot.Birds;
using Shoot.Enemies;
using UnityEngine;
using Edge = Enemies.Edge;

namespace Birds
{
    [RequireComponent(typeof(BirdMover))]
    [RequireComponent(typeof(BirdShooter))]
    [RequireComponent(typeof(BirdCollisionHandler))]
    [RequireComponent(typeof(BirdInputHandler))]
    public class Bird : MonoBehaviour, IHittable
    {
        private BirdMover _mover;
        private BirdShooter _shooter;
        private BirdCollisionHandler _collisionHandler;
        private BirdInputHandler _inputHandler;

        public event Action GameOver;

        private void Awake()
        {
            _mover = GetComponent<BirdMover>();
            _shooter = GetComponent<BirdShooter>();
            _collisionHandler = GetComponent<BirdCollisionHandler>();
            _inputHandler = GetComponent<BirdInputHandler>();
        }

        private void OnEnable()
        {
            _inputHandler.Moved += Move;
            _inputHandler.Shot += Shoot;
            _collisionHandler.CollisionDetected += ProcessCollision;
        }

        private void OnDisable()
        {
            _inputHandler.Moved -= Move;
            _inputHandler.Shot -= Shoot;
            _collisionHandler.CollisionDetected -= ProcessCollision;
        }

        private void Move() => 
            _mover.Move();

        private void Shoot() => 
            _shooter.Shoot();

        private void ProcessCollision(IInteractable interactable)
        {
            if (interactable is Enemy or EnemyBullet or Edge)
                GameOver?.Invoke();
        }

        public void Reset()
        {
            _mover.Reset();
            _shooter.Reset();
        }
    }
}