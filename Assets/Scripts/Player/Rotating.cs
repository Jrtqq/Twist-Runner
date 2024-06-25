using System.Collections;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class Rotating : MonoBehaviour
    {
        [SerializeField] private float _defaultSpeed;
        [SerializeField] private float _maxRotationRadius;
        [SerializeField] private float _snapThreshold;

        [Header("Technical")]
        [SerializeField] private LayerMask _column;
        [SerializeField] private Transform _transform;

        private float _speed;
        private float _currentTurnSide;
        private float _currentTurnRadius;
        private Vector3 _currentCenterOffset;
        private Quaternion _currentAngle;
        private Transform _rotationCenter = null;

        private bool _isRotating = false;

        public bool TurnSide => _currentTurnSide == 1;

        private void Awake()
        {
            _transform = transform;
            _speed = _defaultSpeed;
        }

        private void FixedUpdate()
        {
            if (_isRotating)
            {
                MoveAngular();
                TurnByDirection();
            }
        }

        public bool TryFindColumn(out Transform column)
        {
            column = Physics.OverlapSphere(_transform.position, _maxRotationRadius, _column).FirstOrDefault()?.transform;
            return column != null;
        }

        public void StartRotation(Transform center)
        {
            _isRotating = true;
            _rotationCenter = center;
            _currentCenterOffset = transform.position - _rotationCenter.position;

            _currentTurnSide = Mathf.Sign(transform.InverseTransformPoint(_rotationCenter.position).x);
            _currentTurnRadius = _currentCenterOffset.magnitude;
            _currentAngle = Quaternion.Euler(0, (_speed / _currentTurnRadius * Mathf.Rad2Deg) * Time.fixedDeltaTime * _currentTurnSide, 0);
        }

        public void StopRotation()
        {
            _isRotating = false;
            AlignDirection();
        }

        public IEnumerator SpeedUp(float percentage, float duration)
        {
            _speed += _defaultSpeed * percentage;
            yield return new WaitForSeconds(duration);
            _speed = _defaultSpeed;
        }

        private void MoveAngular()
        {
            _currentCenterOffset = _currentAngle * _currentCenterOffset;
            transform.position = _rotationCenter.position + _currentCenterOffset;
        }

        private void TurnByDirection()
        {
            transform.LookAt(_rotationCenter.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            transform.Rotate(0, -90 * _currentTurnSide, 0);
        }

        private void AlignDirection()
        {
            float snappedY = Mathf.Round(_transform.eulerAngles.y / 90) * 90;

            if (Mathf.Abs(snappedY - _transform.eulerAngles.y) <= _snapThreshold)
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, snappedY, transform.eulerAngles.z);
        }
    }
}