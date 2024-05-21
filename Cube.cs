using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isColorChanged;

    public bool IsColorChanged => _isColorChanged;

    private void Start()
    {
        _isColorChanged = false;
    }

    public void SetColorChangeStatus(bool isColorChanged)
    {
        _isColorChanged = isColorChanged;
    }
}
