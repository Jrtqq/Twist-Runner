using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(LineRenderer))]

    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private LineRenderer _lineRenderer;

        private Vector3 _columnPosition = Vector3.zero;

        private void Awake()
        {
            _lineRenderer.enabled = false;
        }

        private void Update()
        {
            if (_lineRenderer.enabled == true)
            {
                _lineRenderer.SetPositions(new Vector3[] { _transform.position, _columnPosition });
            }
        }

        public void StartRotation(Vector3 columnPosition)
        {
            //анимации
            _lineRenderer.enabled = true;
            _columnPosition = columnPosition;
        }

        public void StopRotation()
        {
            _lineRenderer.enabled = false;
            _columnPosition = Vector3.zero;
        }
    }
}