using UnityEngine;

/// <summary>
/// This is an example of where null pointer errors can creep into Unity code from Serialized fields.
/// </summary>
public class NullPointerSerializedExceptionTest : MonoBehaviour
{
    [Tooltip("This referenced is serialized, you need to link a GameObject with the NullPointerEnemy Component in the editor")]
    [SerializeField] public NullPointerMonoBehaviourEnemy _serializedEnemy;
    
    void Start()
    {
        int health = 0;
        int healthGuarded = 0;

        health = GetHealth();
        healthGuarded = GetHealthWithNullGuarding();

        Debug.Log($"Health: [{health}]. Health with null guard: [{healthGuarded}]");
    }

    // BAD! This will fail unless the reference has been set in the editor.
    private int GetHealth()
    {
        return _serializedEnemy.GetHealth();
    }

    // GOOD! If there is a null exception, this will log a warning and return early with a -1 value.
    // -1 is a typical value that tells us something is wrong.
    private int GetHealthWithNullGuarding()
    {
        if (_serializedEnemy == null)
        {
            Debug.LogWarning($"Attempting to get Health value from NullPointerEnemy, but there's a null reference!");
            return -1;
        }

        return _serializedEnemy.GetHealth();
    }
}
