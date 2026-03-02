using UnityEngine;

/// <summary>
/// This is an example of a null pointer error completely stopping code from running.
/// </summary>
public class NullPointerExceptionTest : MonoBehaviour
{
    private NullPointerEnemy _initializedEnemy;

    void Start()
    {
        int health;

        // [CAUTION!] This line will break!
        // Add a breakpoint, look at _initializedEnemy...
        health = _initializedEnemy.GetHealth();

        // SO - Initialize before we access _serializedEnemy.
        _initializedEnemy = new NullPointerEnemy();
        health = _initializedEnemy.GetHealth();

        Debug.Log($"Got health: [{health}]");
    }
}
