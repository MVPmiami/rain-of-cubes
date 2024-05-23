using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class RainSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefub;
    [SerializeField, Range(-20, 0)] private int _minOffset;
    [SerializeField, Range(0, 20)] private int _maxOffset;
    [SerializeField, Range(20, 50)] private int _yOffset;
    [SerializeField, Range(0.01f, 10)] private float _repeatRate;
    [SerializeField, Range(1, 5000)] private int _poolCapacity;
    [SerializeField, Range(1, 5000)] private int _poolMaxSize;

    private float _minLiveTime;
    private float _maxLiveTime;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefub),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
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
        float cubeliveTime = Random.Range(_minLiveTime, _maxLiveTime);

        StartCoroutine(ReturnToPool(cube, cubeliveTime));
    }

    private void ActionOnGet(Cube cube)
    {
        GameObject obj = cube.gameObject;
        obj.transform.position = new Vector3(Random.Range(_minOffset, _maxOffset), transform.position.y + _yOffset, Random.Range(_minOffset, _maxOffset));
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