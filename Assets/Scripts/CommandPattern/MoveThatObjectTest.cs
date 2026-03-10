using Unity.VisualScripting;
using UnityEngine;

public class MoveThatObjectTest : MonoBehaviour
{
    private IMoveObjectInterface _Move;
    private IMoveObjectInterface _debug;
    [SerializeField] private GameObject _objectToMove;

    private void Awake()
    {
        _Move = new ObjectMover(_objectToMove);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _Move.MoveObject(true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _Move.MoveObject(false);
        }
    }
}
