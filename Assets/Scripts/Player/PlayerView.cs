using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(LineRenderer))]

    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Animator _animator;

        private const string JumpTrigger = "Jump";
        private const string RunTrigger = "Run";
        private const string FinishTrigger = "Finish";
        private const string RotationBool = "Rotating";
        private const string TurnSideBool = "TurnSide";

        private Vector3 _columnPosition = Vector3.zero;
        private float _yOffset = 1;

        private void Awake()
        {
            _lineRenderer.enabled = false;
        }

        private void Update()
        {
            if (_lineRenderer.enabled == true)
            {
                Vector3 startPoint = _transform.position + new Vector3(0, _yOffset, 0);
                _lineRenderer.SetPositions(new Vector3[] { startPoint, (startPoint + _columnPosition) / 2, _columnPosition });
            }
        }

        public void StartRotation(Vector3 columnPosition, bool turnSide)
        {
            _animator.SetBool(RotationBool, true);
            _animator.SetBool(TurnSideBool, turnSide);

            _lineRenderer.enabled = true;
            _columnPosition = columnPosition;
        }

        public void StopRotation()
        {
            _animator.SetBool(RotationBool, false);

            _lineRenderer.enabled = false;
            _columnPosition = Vector3.zero;
        }

        public void Jump()
        {
            _animator.SetTrigger(JumpTrigger);
        }

        public void Run()
        {
            _animator.SetTrigger(RunTrigger);
        }

        public void Finish()
        {
            _animator.SetTrigger(FinishTrigger);
        }
    }
}