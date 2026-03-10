using JetBrains.Annotations;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class OPPlayer : MonoBehaviour, OP_IBullet
{
    [SerializeField, Range(1, 50)] private float _moveSpeed = 1;
    [SerializeField] private OPBulletPool _bulletPoolRef;

    [Tooltip("50 = One Second Delay")]
    [SerializeField, Range(5, 50)] private float _bulletDelay = 10;
    
    private ObjectPool<GameObject> _pool;
    private Rigidbody rb;
    private float _horizontalInput;
    private float _verticalInput;
    private float _coolDownTimer;
    private bool _shoot = false;
    private bool _onCoolDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InitComponents();
    }

    // Update is called once per frame
    private void Update()
    {
        Inputs();
    }


    private void FixedUpdate()
    {
        ShootBullet();
    }

    private void InitComponents()
    {
        rb = GetComponent<Rigidbody>();
        _pool = _bulletPoolRef.GetPool();

       GameObject[] _walls = GameObject.FindGameObjectsWithTag("Wall");
       foreach(GameObject wall in _walls)
       {
            wall.GetComponent<OP_Wall>()._bulletPool = _pool;
       }
    }

    private void Inputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        if(Input.GetAxis("Fire1") != 0)
        {
            _shoot = true;
        }
        else
        {
            _shoot = false;
        }
    }

    private void ShootBullet()
    {
        if (_coolDownTimer >= 0)
        {
            _onCoolDown = true;
            _coolDownTimer--;
        }
        else if (_onCoolDown)
        {
            _onCoolDown = false;
        }

        rb.AddForce(_horizontalInput * _moveSpeed, _verticalInput * _moveSpeed, 0);
        if (_shoot && !_onCoolDown)
        {
            GameObject _bullet = _pool.Get();
            _bullet.GetComponent<OP_Bullet>().Shoot(500, 5);
            Vector3 _bulletPosition = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, 0);
            _bullet.transform.position = _bulletPosition;
            _coolDownTimer = _bulletDelay;
        }
    }
}
