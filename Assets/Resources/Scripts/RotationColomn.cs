using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotationColomn : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _player.transform.parent = _transform;
            _player.enabled = false;
        }
        
        if (Input.anyKey)
        {
            _transform.Rotate(0, 1, 0);
        }
    }
}
