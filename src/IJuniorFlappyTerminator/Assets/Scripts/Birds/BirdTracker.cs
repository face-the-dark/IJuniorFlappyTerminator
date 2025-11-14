using UnityEngine;

namespace Birds
{
    public class BirdTracker : MonoBehaviour
    {
        [SerializeField] private BirdMover _birdMover;
        [SerializeField] private float _offsetX = 1f;
        
        private void LateUpdate()
        {
            Vector3 transformPosition = transform.position;
            transformPosition.x = _birdMover.transform.position.x + _offsetX;
            transform.position = transformPosition;
        }
    }
}