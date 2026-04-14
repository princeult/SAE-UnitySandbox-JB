using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateShape()
    {
        _vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0)
        };

        _triangles = new int[]
        {
            0, 1, 2  
        };
    }

    private void UpdateMesh()
    {
        if(_mesh = null) 
        {
            _mesh = new Mesh
            {
                vertices = _vertices,
                triangles = _triangles
                
            };
            _mesh.RecalculateNormals();
            
        }
        // _mesh.Clear();
        // _mesh.vertices = _vertices;
        // _mesh.triangles = _triangles;

        
    }
}
