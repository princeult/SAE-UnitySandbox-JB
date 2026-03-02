using UnityEngine;

public class NullPointerMonoBehaviourEnemy : MonoBehaviour
{
    private int _health = 5;

    public int GetHealth()
    {
        return _health;
    }
}

// Not this is NOT inheriting from MonoBehaviour!
public class NullPointerEnemy
{
    private int _health = 6;

    public int GetHealth()
    {
        return _health;
    }
}