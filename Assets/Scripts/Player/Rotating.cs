using System.Collections;
using System.Linq;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _maxRotationRadius;
    [SerializeField] private float _snapThreshold;

    [Header("Technical")]
    [SerializeField] private LayerMask _column;
    [SerializeField] private Transform _transform;

    private float _speed;
    private float _currentTurnSide = 0;
    private Transform _rotationCenter = null;


    public Vector3 RotationCenter => _rotationCenter.position;
    public bool IsRotating { get; private set; } = false;

    private void Awake()
    {
        _transform = transform;
        _speed = _defaultSpeed;
    }

    private void Update()
    {
        if (IsRotating)
        {
            _rotationCenter.Rotate(0, _speed * Time.deltaTime * _currentTurnSide, 0);
        }
    }

    public bool TryFindColumn(out Transform column)
    {
        column = Physics.OverlapSphere(_transform.position, _maxRotationRadius, _column).FirstOrDefault()?.transform;
        return column != null;
    }

    public void StartRotation(Transform center)
    {
        IsRotating = true;
        _rotationCenter = center.transform;
        _transform.SetParent(_rotationCenter);

        _currentTurnSide = Mathf.Sign(_transform.InverseTransformPoint(_rotationCenter.position).x);
        _transform.rotation = Quaternion.LookRotation(center.transform.position - _transform.position) * Quaternion.Euler(0, -90 * _currentTurnSide, 0);
    }

    public void StopRotation()
    {
        IsRotating = false;
        _rotationCenter = null;
        _transform.SetParent(null);
    }

    public void AlignDirection()
    {
        float snappedY = Mathf.Round(_transform.eulerAngles.y / 90) * 90;

        if (Mathf.Abs(snappedY - _transform.eulerAngles.y) <= _snapThreshold)
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, snappedY, transform.eulerAngles.z);
    }

    public IEnumerator SpeedUp(float percentage, float duration)
    {
        _speed += _defaultSpeed * percentage;
        yield return new WaitForSeconds(duration);
        _speed = _defaultSpeed;
    }
}