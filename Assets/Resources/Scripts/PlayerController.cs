using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))] 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private PlayerControls _controls;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();

        _controls.Player.Turn.performed += OnPerformed; 
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        _rigidbody.velocity = _transform.forward * _speed;


    }

    private void OnPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("робит");
    }
}
