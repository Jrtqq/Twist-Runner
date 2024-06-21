using System.Collections;
using UnityEngine;

namespace Player
{
    public class Running : MonoBehaviour
    {
        [field: SerializeField] public float SpeedBoost { get; private set; }
        [field: SerializeField] public float DefaultSpeed { get; private set; }
        [field: SerializeField] public float BoostDuration { get; private set; }

        [SerializeField] private Transform _transform;

        private float _speed;

        private void Awake()
        {
            _transform = transform;
            _speed = DefaultSpeed;
        }

        private void FixedUpdate()
        {
            transform.position += _transform.forward * _speed * Time.fixedDeltaTime;
        }

        public IEnumerator SpeedUp()
        {
            _speed += SpeedBoost;
            yield return new WaitForSeconds(BoostDuration);
            _speed = DefaultSpeed;
        }
    }
}
