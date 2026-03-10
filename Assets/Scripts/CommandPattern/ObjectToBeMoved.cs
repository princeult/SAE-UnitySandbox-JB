using UnityEngine;

public class ObjectToBeMoved : MonoBehaviour, IMoveObjectInterface
{
    public void MoveObject(bool _directon)
    {
        if (_directon)
        {
            transform.position = transform.position + new Vector3( 1, 0, 0 );
        }
        else
        {
            transform.position = transform.position + new Vector3( -1, 0, 0 );
        }
    }

    public void PrintToConsole()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
