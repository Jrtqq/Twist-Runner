using System.Collections;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private AnimationCurve _jumpCurve;

    public float JumpDuration => _jumpCurve.keys[^1].time;

    public IEnumerator Jump()
    {
        float time = 0;

        while (time < JumpDuration)
        {
            _transform.position = new(_transform.position.x, _jumpCurve.Evaluate(time), transform.position.z);
            time += Time.deltaTime;

            yield return null;
        }
    }
}
