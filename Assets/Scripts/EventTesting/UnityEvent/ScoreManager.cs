using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PressurePlate _plate;  // Drag in via Inspector

    private int _score = 0;

    // Subscribe when this object becomes active.
    // OnEnable runs AFTER Awake but BEFORE Start on first activation.
    private void OnEnable()
    {
        _plate.OnActivated.AddListener(AddScore);
    }

    // Always unsubscribe in OnDisable.
    // If you do not, disabled objects may still receive events
    // and cause null reference errors.
    private void OnDisable()
    {
        _plate.OnActivated.RemoveListener(AddScore);
    }

    private void AddScore()
    {
        _score += 10;
        Debug.Log("Score: " + _score);
    }
}