using UnityEngine;

public class Cube : MonoBehaviour
{
    private const string Color = "_Color";

    private bool _isColorChanged;

    private void Start()
    {
        _isColorChanged = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isColorChanged && collision.gameObject.TryGetComponent(out Platform platform))
        {
            Renderer renderer = GetComponent<Renderer>();
            Color _color = Random.ColorHSV();
            renderer.material.SetColor(Color, _color);
            _isColorChanged = true;
        }
    }
}
