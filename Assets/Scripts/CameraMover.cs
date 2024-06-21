using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _zOffset;

    [Header("Technical")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _player;

    private void Update()
    {
        _transform.position = new Vector3(_player.position.x, _transform.position.y, _player.position.z + _zOffset);
    }
}
