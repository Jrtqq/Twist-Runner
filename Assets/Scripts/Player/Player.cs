using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Running), typeof(Rotating), typeof(Jumper))]
    [RequireComponent(typeof(PlayerView))]

    public class Player : MonoBehaviour
    {
        [SerializeField] private Running _mover;
        [SerializeField] private Rotating _rotator;
        [SerializeField] private Jumper _jumper;

        [SerializeField] private PlayerView _view;

        private PlayerControls _input;

        private void Awake()
        {
            _input = new();
            _mover.enabled = true;
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Turn.performed += OnTurnButton;
            _input.Player.Turn.canceled += OnTurnButtonRelease;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Turn.performed -= OnTurnButton;
            _input.Player.Turn.canceled -= OnTurnButtonRelease;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out JumpPad _))
            {
                StopRotation();
                StartCoroutine(_jumper.Jump());
                _view.Jump();
            }
            else if (other.TryGetComponent(out SpeedBooster _))
            {
                StartCoroutine(_mover.SpeedUp());
                StartCoroutine(_rotator.SpeedUp(_mover.SpeedBoost / _mover.DefaultSpeed, _mover.BoostDuration));
            }
        }

        private void OnTurnButton(InputAction.CallbackContext context)
        {
            if (_jumper.IsJumping == false && _rotator.TryFindColumn(out Transform column))
                StartRotation(column);
        }
        
        private void OnTurnButtonRelease(InputAction.CallbackContext context)
        {
            if (_jumper.IsJumping == false)
                StopRotation();
        }

        private void StartRotation(Transform column)
        {
            _mover.enabled = false;
            _rotator.StartRotation(column);
            _view.StartRotation(column.position, _rotator.TurnSide);
        }

        private void StopRotation()
        {
            _mover.enabled = true;
            _rotator.StopRotation();
            _view.StopRotation();
        }
    }
}
