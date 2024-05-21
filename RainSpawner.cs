using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class RainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefub;

    private float _minLiveTime;
    private float _maxLiveTime;
    private int _minOffset;
    private int _maxOffset;
    private int _yOffset;
    private float _repeatRate;
    private int _poolCapacity;
    private int _poolMaxSize;
    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _minOffset = -20;
        _maxOffset = 20;
        _yOffset = 25;
        _poolCapacity = 5000;
        _poolMaxSize = 5000;
        _repeatRate = 0.01f;
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_cubePrefub),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        _minLiveTime = 2.0f;
        _maxLiveTime = 5.0f;

        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    public void DestroyCube(Cube cube)
    {
        float cubeliveTime = UnityEngine.Random.Range(_minLiveTime, _maxLiveTime);

        if (!cube.IsColorChanged)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            Color _color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
            renderer.material.SetColor("_Color", _color);
            cube.SetColorChangeStatus(true);
        }

        StartCoroutine(ReturnToPool(cube, cubeliveTime));
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = new Vector3(UnityEngine.Random.Range(_minOffset, _maxOffset), transform.position.y + _yOffset, UnityEngine.Random.Range(_minOffset, _maxOffset));
        obj.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private IEnumerator ReturnToPool(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        cube.gameObject.SetActive(false);
    }
}
