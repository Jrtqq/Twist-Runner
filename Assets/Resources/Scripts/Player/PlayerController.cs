using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxRotationRadius;
        [SerializeField] private float _snapThreshold;
        [SerializeField] private LayerMask _column;

        private Transform _transform;

        private float _currentTurnSide = 0;
        private Transform _rotationCenter = null;

        public PlayerControls Controls { get; private set; }
        public Vector3 RotationCenter => _rotationCenter.position;
        public bool IsRunning { get; private set; } = true;

        public event UnityAction RotationStarted;
        public event UnityAction RotationFinished;

        private void Awake()
        {
            _transform = transform;
            Controls = new PlayerControls();
        }

        private void OnEnable()
        {
            Controls.Enable();

            Controls.Player.Turn.performed += OnTurnButton;
        }

        private void OnDisable()
        {
            Controls.Disable();

            Controls.Player.Turn.performed -= OnTurnButton;
        }

        private void Update()
        {
            if (IsRunning)
            {
                transform.position += _transform.forward * _speed * Time.deltaTime;
            }
            else
            {
                if (Controls.Player.Turn.IsPressed() == false)
                {
                    StopRotation();
                    return;
                }

                _rotationCenter.Rotate(0, _rotationSpeed * _currentTurnSide, 0);
            }
        }

        private void OnTurnButton(InputAction.CallbackContext context)
        {
            Collider column = Physics.OverlapSphere(_transform.position, _maxRotationRadius, _column).FirstOrDefault();

            if (column != null)
                StartRotation(column.transform);
        }

        private void StartRotation(Transform column)
        {
            IsRunning = false;
            _rotationCenter = column.transform;
            _transform.SetParent(_rotationCenter);

            _currentTurnSide = Mathf.Sign(_transform.InverseTransformPoint(RotationCenter).x);
            _transform.rotation = Quaternion.LookRotation(column.transform.position - _transform.position) * Quaternion.Euler(0, -90 * _currentTurnSide, 0);

            RotationStarted?.Invoke();
        }

        private void StopRotation()
        {
            IsRunning = true;
            _rotationCenter = null;
            _transform.SetParent(null);

            AlignDirection();

            RotationFinished?.Invoke();
        }

        private void AlignDirection()
        {
            float snappedY = Mathf.Round(_transform.eulerAngles.y / 90) * 90;

            if (Mathf.Abs(snappedY - _transform.eulerAngles.y) <= _snapThreshold)
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, snappedY, transform.eulerAngles.z);
        }
    }
}
