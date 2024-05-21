using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    [SerializeField] private RainSpawner _rainSpawner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.TryGetComponent(out Cube cube))
            _rainSpawner.DestroyCube(cube);
    }
}
