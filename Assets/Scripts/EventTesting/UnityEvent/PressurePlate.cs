using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent OnActivated = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
         OnActivated?.Invoke();
    }
}