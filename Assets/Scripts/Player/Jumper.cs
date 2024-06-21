using System.Collections;
using UnityEngine;

namespace Player
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private AnimationCurve _jumpCurve;

        public bool IsJumping { get; private set; } = false;

        public IEnumerator Jump()
        {
            float time = 0;
            IsJumping = true;

            while (time < _jumpCurve.keys[^1].time)
            {
                _transform.position = new(_transform.position.x, _jumpCurve.Evaluate(time), transform.position.z);
                time += Time.deltaTime;

                yield return null;
            }

            IsJumping = false;
        }
    }
}
