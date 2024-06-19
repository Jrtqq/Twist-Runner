using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(LineRenderer))]

    public class PlayerView : MonoBehaviour
    {
        private Transform _transform;
        private PlayerController _controller;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _transform = transform;
            _controller = GetComponent<PlayerController>();
            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.enabled = false;
        }

        private void OnEnable()
        {
            _controller.RotationStarted += OnRotationStart;
            _controller.RotationFinished += OnRotationFinish;
        }

        private void OnDisable()
        {
            _controller.RotationStarted -= OnRotationStart;
            _controller.RotationFinished -= OnRotationFinish;
        }

        private void Update()
        {
            if (_controller.IsRunning == false)
            {
                _lineRenderer.SetPositions(new Vector3[] { _transform.position, _controller.RotationCenter });
            }
        }

        private void OnRotationStart()
        {
            //анимации
            _lineRenderer.enabled = true;
        }

        private void OnRotationFinish()
        {
            _lineRenderer.enabled = false;
        }
    }
}