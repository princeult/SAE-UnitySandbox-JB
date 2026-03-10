using UnityEngine;

public class ObjectMover : IMoveObjectInterface
{
    private GameObject _gameObject;
    public void MoveObject(bool _d)
    {

        if (_d)
        {
            _gameObject.transform.position = _gameObject.transform.position + new Vector3( 1, 0, 0 );
        }
        else
        {
            _gameObject.transform.position = _gameObject.transform.position + new Vector3( -1, 0, 0 );
        }
    }

    public void PrintToConsole()
    {
        Debug.Log("Ive Printed To The Console");
    }

    public ObjectMover(GameObject go)
    {
        _gameObject = go;
    }
}
