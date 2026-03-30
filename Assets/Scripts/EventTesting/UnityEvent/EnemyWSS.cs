using UnityEngine;

public class EnemyWSS : MonoBehaviour
{

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        GameManagerWSS.OnGamePaused.AddListener(StopMoving);
    }

    private void OnDisable()
    {
        GameManagerWSS.OnGamePaused.RemoveListener(StopMoving);
    }

    private void StopMoving()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.useGravity = false;
        enabled = false;
    }


}
