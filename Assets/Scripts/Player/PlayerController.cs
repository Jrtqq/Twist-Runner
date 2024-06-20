using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        //[SerializeField] private float _defaultSpeed;
        //[SerializeField] private float _defaultRotationSpeed;
        //[SerializeField] private float _maxRotationRadius;
        //[SerializeField] private float _snapThreshold;
        //[SerializeField] private AnimationCurve _jumpCurve;
        //[SerializeField] private float _speedBoost;
        //[SerializeField] private float _boostDuration;

        //[SerializeField] private LayerMask _column;

        //public float _speed;
        //public float _rotationSpeed;
        //private float _currentTurnSide = 0;
        //private Transform _rotationCenter = null;
        //private PlayerControls _controls;

        //public Vector3 RotationCenter => _rotationCenter.position;
        //public bool IsRunning { get; private set; } = true;

        //public event Action RotationStarted;
        //public event Action RotationFinished;
        //public event Action<float> Jumped;

        //private void Awake()
        //{
        //    _controls = new PlayerControls();
        //    _speed = _defaultSpeed;
        //    _rotationSpeed = _defaultRotationSpeed;
        //}

        //private void OnEnable()
        //{
        //    _controls.Enable();

        //    _controls.Player.Turn.performed += OnTurnButton;
        //}

        //private void OnDisable()
        //{
        //    _controls.Disable();

        //    _controls.Player.Turn.performed -= OnTurnButton;
        //}

        //private void Update()
        //{
        //    if (IsRunning)
        //    {
        //        transform.position += _transform.forward * _speed * Time.deltaTime;
        //    }
        //    else
        //    {
        //        if (_controls.Player.Turn.IsPressed() == false)
        //        {
        //            StopRotation();
        //            return;
        //        }

        //        _rotationCenter.Rotate(0, _rotationSpeed * Time.deltaTime * _currentTurnSide, 0);
        //    }
        //}

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.TryGetComponent(out JumpPad _))
        //        StartCoroutine(Jump());
        //    else if (other.TryGetComponent(out SpeedBooster _))
        //        StartCoroutine(SpeedUp());
        //}

        //private void OnTurnButton(InputAction.CallbackContext context)
        //{
        //    Collider column = Physics.OverlapSphere(_transform.position, _maxRotationRadius, _column).FirstOrDefault();

        //    if (column != null)
        //        StartRotation(column.transform);
        //}

        //private IEnumerator Jump()
        //{
        //    Jumped?.Invoke(_jumpCurve.keys[^1].time);

        //    float time = 0;

        //    while (time < _jumpCurve.keys[^1].time)
        //    {
        //        _transform.position = new(_transform.position.x, _jumpCurve.Evaluate(time), transform.position.z);
        //        time += Time.deltaTime;

        //        yield return null;
        //    }
        //}

        //private IEnumerator SpeedUp()
        //{
        //    _speed += _speedBoost;
        //    _rotationSpeed += (_speedBoost / _defaultSpeed) * _rotationSpeed;

        //    yield return new WaitForSeconds(_boostDuration);

        //    _speed = _defaultSpeed;
        //    _rotationSpeed = _defaultRotationSpeed;
        //}

        //private void StartRotation(Transform column)
        //{
        //    IsRunning = false;
        //    _rotationCenter = column.transform;
        //    _transform.SetParent(_rotationCenter);

        //    _currentTurnSide = Mathf.Sign(_transform.InverseTransformPoint(RotationCenter).x);
        //    _transform.rotation = Quaternion.LookRotation(column.transform.position - _transform.position) * Quaternion.Euler(0, -90 * _currentTurnSide, 0);

        //    RotationStarted?.Invoke();
        //}

        //private void StopRotation()
        //{
        //    IsRunning = true;
        //    _rotationCenter = null;
        //    _transform.SetParent(null);

        //    AlignDirection();

        //    RotationFinished?.Invoke();
        //}

        //private void AlignDirection()
        //{
        //    float snappedY = Mathf.Round(_transform.eulerAngles.y / 90) * 90;

        //    if (Mathf.Abs(snappedY - _transform.eulerAngles.y) <= _snapThreshold)
        //        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, snappedY, transform.eulerAngles.z);
        //}
    }
}
