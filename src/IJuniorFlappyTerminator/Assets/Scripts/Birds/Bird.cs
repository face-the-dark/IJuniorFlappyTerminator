using System;
using Enemies;
using Interactable;
using Shoot.Enemies;
using UnityEngine;

namespace Birds
{
    [RequireComponent(typeof(BirdMover))]
    [RequireComponent(typeof(BirdCollisionHandler))]
    [RequireComponent(typeof(ScoreCounter))]
    public class Bird : MonoBehaviour
    {
        private BirdMover _birdMover;
        private BirdCollisionHandler _handler;
        private ScoreCounter _scoreCounter;

        public event Action GameOver;

        private void Awake()
        {
            _birdMover = GetComponent<BirdMover>();
            _handler = GetComponent<BirdCollisionHandler>();
            _scoreCounter = GetComponent<ScoreCounter>();
        }

        private void OnEnable() => 
            _handler.CollisionDetected += ProcessCollision;

        private void OnDisable() => 
            _handler.CollisionDetected -= ProcessCollision;

        private void ProcessCollision(IInteractable interactable)
        {
            if (interactable is Enemy or EnemyBullet or Edge)
                GameOver?.Invoke();
        }

        public void Reset()
        {
            _scoreCounter.Reset();
            _birdMover.Reset();
        }
    }
}