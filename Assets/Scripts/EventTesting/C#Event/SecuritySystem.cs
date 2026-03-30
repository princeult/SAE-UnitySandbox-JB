using UnityEngine;

public class SecuritySystem : MonoBehaviour
{
    public delegate void SecurityAction();

    public static event SecurityAction OnSecurityBreach;

    private void OnTriggerEnter(Collider other)
    {
        OnSecurityBreach?.Invoke();
    }
    
}
