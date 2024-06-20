using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player.Player _))
            Destroy(gameObject);
    }
}
