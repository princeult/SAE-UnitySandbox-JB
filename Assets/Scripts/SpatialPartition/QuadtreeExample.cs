using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class QuadtreeExample : MonoBehaviour
{
    public int objectCount = 2;
    public GameObject Object;
    private Quadtree Tree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tree = new Quadtree();
        for(int i = 0; i < objectCount; i++)
        {
        Debug.Log(Tree.Insert(Instantiate(Object, new Vector3(Random.Range(1f,9f), Random.Range(1f,9f), 0),  Quaternion.identity), Tree.root) + " " + i);
        }

        Debug.Log("was");
        //Tree.Retrive(Tree.root); FIX THIS
    }   

    // Update is called once per frame
    void Update()
    {
    }
}

public class QuadTreeNode
{
    public Rect bounds = new (0, 0, 10, 10);
    public List<GameObject> objects = new List<GameObject>();
    public QuadTreeNode[] children = new QuadTreeNode[0];

    public int currentDepth;

    public QuadTreeNode[] Split(QuadTreeNode _node)
    {
        QuadTreeNode _one= new();
        QuadTreeNode _two= new();
        QuadTreeNode _three= new();
        QuadTreeNode _four = new();
        QuadTreeNode[] _newNodes =  new QuadTreeNode[] {_one, _two, _three, _four};

        
        for(int i = 0; i < 4; i++)
        {
            _newNodes[i].currentDepth = _node.currentDepth + 1;
            switch (i)
            {
                case 0:
                    _newNodes[i].bounds = new Rect(bounds.position,bounds.size/2f);;
                    break;
                case 1:
                    _newNodes[i].bounds = new Rect(new Vector2(bounds.position.x + bounds.width/2f, bounds.position.y),bounds.size/2f);
                    break;
                case 2:
                    _newNodes[i].bounds = new Rect(new Vector2(bounds.position.x, bounds.position.y + bounds.height/2f), bounds.size/2f);
                    break;
                case 3:
                    _newNodes[i].bounds = new Rect(new Vector2(bounds.position.x + bounds.width/2f, bounds.position.y + bounds.height/2f), bounds.size/2f);
                    break;
            }

        }

        return _newNodes;
    }
}

public class Quadtree
{
    public QuadTreeNode root = new QuadTreeNode();
    public int maxObjectsPerNode = 5;
    public int maxDepth = 3;

    public bool Insert(GameObject _objectToInsert, QuadTreeNode _location)
    {

        if (!_location.bounds.Contains(_objectToInsert.transform.position))
        {
            return false;
        }
        else
        {
            if(_location.children.Length == 0)
            {
                if(_location.objects.Count >= maxObjectsPerNode && _location.currentDepth < maxDepth)
                {
                    
                    _location.children = _location.Split(_location);
                    foreach(GameObject _objectToMove in _location.objects)
                    {
                        foreach(QuadTreeNode _child in _location.children)
                        {
                            Insert(_objectToMove, _child);
                        }
                    }
                    _location.objects = null;
                    foreach(QuadTreeNode _child in _location.children)
                    {
                        if(Insert(_objectToInsert, _child))
                        {
                            return true;
                        }
                    }
                    
                }
                else
                {
                    _location.objects.Add(_objectToInsert);
                    return true;
                }
                
            }
            else
            {
                foreach(QuadTreeNode _node in _location.children)
                {
                    if (Insert(_objectToInsert, _node))
                    {
                            return true;
                    }
                }
            }

            return false;
        }
    }

    public List<GameObject> Retrive(QuadTreeNode _node)
    {
        List<GameObject> _temp = new List<GameObject>();
        

        if (root.children.Length > 0)
            {
                foreach(QuadTreeNode _child in root.children)
                {
                    foreach(GameObject __child in Retrive(_child))
                    {
                        _temp.Add(__child);
                    }
                }
            }
            else if (root.children.Length == 0)
            {
                foreach(GameObject _object in root.objects)
                {
                    _temp.Add(_object);
                }
            }

            if (_node.children.Length > 0)
            {
                foreach(QuadTreeNode _child in _node.children)
                {
                    foreach(GameObject __child in Retrive(_child))
                    {
                        _temp.Add(__child);
                    }
                }
            }
            else if (_node.children.Length == 0)
            {
                foreach(GameObject _object in _node.objects)
                {
                    _temp.Add(_object);
                }
            }

        return _temp;
    }
}

