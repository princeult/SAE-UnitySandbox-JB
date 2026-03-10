using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class OP_Wall : MonoBehaviour
{
    [DoNotSerialize] public ObjectPool<GameObject> _bulletPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _bulletPool.Release(collision.gameObject);
        }
    }
}
