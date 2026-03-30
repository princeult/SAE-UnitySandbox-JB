using UnityEngine;

[RequireComponent(typeof(Light))]
public class SecurityLight : MonoBehaviour
{
    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }
    private void OnEnable()
    {
        SecuritySystem.OnSecurityBreach += ActivateLight;
    }

    // Always unsubscribe. -= removes this method from the event.
    private void OnDisable()
    {
        SecuritySystem.OnSecurityBreach -= ActivateLight;
    }

    private void ActivateLight()
    {
        _light.intensity = 10f;
        _light.color = Color.red;
    }

}
