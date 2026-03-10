using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OP_Bullet : MonoBehaviour, OP_IBullet
{
    private Rigidbody rb;
    private float damage;
    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Shoot(float _speed, float _damage)
    {
        rb.AddForce(_speed, 0, 0);
        damage = _damage;
    }
}
