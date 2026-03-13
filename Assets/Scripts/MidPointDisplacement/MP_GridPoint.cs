using TMPro;
using UnityEngine;
public class MP_GridPoint : MonoBehaviour
{
    public Vector2 CurrentPos;
    public TextMeshPro DisplayNumber;
    public float Height = 0;
    public void UpdateHeight(float _h)
    {
        if(Height == 0)
        {
        Height = _h;
        transform.position = new Vector3(transform.position.x, Height, transform.position.z);
        }
    }
}

