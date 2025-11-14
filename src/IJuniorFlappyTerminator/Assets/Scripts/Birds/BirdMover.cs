using UnityEngine;

namespace Birds
{
    [RequireComponent(typeof(BirdInputHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BirdMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 2.5f;
        [SerializeField] private float _flyForce = 5f;
    
        [SerializeField] private float _rotaionSpeed = 1f;
        [SerializeField] private float _minRotationZ = -45f;
        [SerializeField] private float _maxRotationZ = 45f;
    
        private BirdInputHandler _birdInputHandler;
        private Rigidbody2D _rigidbody2D;
    
        private Vector3 _startPosition;
        private Quaternion _minRotation;
        private Quaternion _maxRotation;

        private void Awake()
        {
            _birdInputHandler = GetComponent<BirdInputHandler>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _startPosition = transform.position;
            
            _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
            _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        }

        private void OnEnable() =>
            _birdInputHandler.Moved += OnMoved;

        private void OnDisable() => 
            _birdInputHandler.Moved -= OnMoved;

        private void Update() => 
            transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotaionSpeed * Time.deltaTime);

        private void OnMoved()
        {
            _rigidbody2D.velocity = new Vector2(_speed, _flyForce);
            transform.rotation = _maxRotation;
        }

        public void Reset()
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}