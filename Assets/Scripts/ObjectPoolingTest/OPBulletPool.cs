using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class OPBulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPF;
    private ObjectPool<GameObject> bullets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        bullets = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 10,
            maxSize: 50

        );
    }

    public ObjectPool<GameObject> GetPool()
    {
        return bullets;
    }

    public void OnDestroyItem(GameObject _bullet)
    {
        Destroy(_bullet);
    }

    private void OnRelease(GameObject _bullet)
    {
        _bullet.SetActive(false);
    }

    private void OnGet(GameObject _bullet)
    {
        //_bullet.gameObject.SetActive(true);
        _bullet.SetActive(true);
    }
    private GameObject CreateItem()
    {
        GameObject bullet = GameObject.Instantiate(_bulletPF);
        bullet.SetActive(false);
        return bullet;
    }
}
