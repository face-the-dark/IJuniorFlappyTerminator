using System;
using Interactable;
using UnityEngine;

namespace Birds
{
    public class BirdCollisionHandler : MonoBehaviour
    {
        public event Action<IInteractable> CollisionDetected;

        private void OnValidate() => 
            GetComponent<Collider2D>().isTrigger = true;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable)) 
                CollisionDetected?.Invoke(interactable);
        }
    }
}