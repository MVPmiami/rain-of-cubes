using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isColorChanged;

    private void Start()
    {
        _isColorChanged = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) && !this._isColorChanged)
        {
            Renderer renderer = this.GetComponent<Renderer>();
            Color _color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
            renderer.material.SetColor("_Color", _color);
            this._isColorChanged = true;
        }
    }
}
