using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))] 

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationRadius;
    [SerializeField] private float _snapThreshold;
    [SerializeField] private LayerMask _column;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private PlayerControls _controls;
    private LineRenderer _lineRenderer;

    private bool _isRunning = true;
    private Transform _rotationCenter = null;

    private float _currentTurnSide = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _transform = transform;
        _controls = new PlayerControls();

        _rigidbody.maxAngularVelocity = 0;
    }

    private void OnEnable()
    {
        _controls.Enable();

        _controls.Player.Turn.performed += StartRotation; 
    }

    private void OnDisable()
    {
        _controls.Disable();

        _controls.Player.Turn.performed -= StartRotation;
    }

    private void Update()
    {
        Debug.Log(_rigidbody.angularVelocity);

        if (_isRunning)
        {
            _rigidbody.velocity = _transform.forward * _speed;
        }
        else
        {
            if (_controls.Player.Turn.IsPressed() == false)
            {
                StopRotation();
                return;
            }

            _rotationCenter.Rotate(0, _rotationSpeed * _currentTurnSide, 0);
            _lineRenderer.SetPositions(new Vector3[] {_transform.position, _rotationCenter.position});
        }
    }

    private void StartRotation(InputAction.CallbackContext context)
    {
        Collider column = Physics.OverlapSphere(_transform.position, _maxRotationRadius, _column).FirstOrDefault();

        if (column != null)
        {
            _isRunning = false;
            _rotationCenter = column.transform;
            _transform.SetParent(_rotationCenter);
            _lineRenderer.enabled = true;

            _rigidbody.velocity = Vector3.zero;
            _currentTurnSide = Mathf.Sign(_transform.InverseTransformPoint(_rotationCenter.position).x);

            //_transform.rotation = Quaternion.LookRotation(column.transform.position - _transform.position) * Quaternion.Euler(0, -90, 0);
        }
    }

    private void StopRotation()
    {
        _isRunning = true;
        _rotationCenter = null;
        _transform.SetParent(null);
        _lineRenderer.enabled = false;

        float snappedY = Mathf.Round(_transform.eulerAngles.y / 90) * 90;

        if (Mathf.Abs(snappedY - _transform.eulerAngles.y) <= _snapThreshold)
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, snappedY, transform.eulerAngles.z);
    }
}
