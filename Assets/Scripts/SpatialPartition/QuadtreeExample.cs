using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class QuadtreeExample : MonoBehaviour
{
    public int objectCount = 10;
    public GameObject Object;
    private Quadtree Tree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tree = new Quadtree();
        for(int i = 0; i < objectCount; i++)
        {
            Tree.Insert(Instantiate(Object, new Vector3(Random.Range(0,10), Random.Range(0,10), 0),  Quaternion.identity), Tree.root);
        }
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
        
        QuadTreeNode _one = _node;
        QuadTreeNode _two = _node;
        QuadTreeNode _three = _node;
        QuadTreeNode _four = _node;
        QuadTreeNode[] _newNodes =  new QuadTreeNode[] {_one, _two, _three, _four};

        
        
        for(int i = 0; i < 4; i++)
        {
            _newNodes[i].bounds.width /= 2;
            _newNodes[i].bounds.height /= 2;
            Vector2 _newPos = _node.bounds.position;
            switch (i)
            {
                case 0:
                    _newPos += new Vector2(-_node.bounds.width /2, -_node.bounds.height /2);
                    break;
                case 2:
                    _newPos += new Vector2(_node.bounds.width /2, -_node.bounds.height /2);
                    break;
                case 3:
                    _newPos += new Vector2(-_node.bounds.width /2, _node.bounds.height /2);
                    break;
                case 4:
                    _newPos += new Vector2(_node.bounds.width /2, _node.bounds.height /2);
                    break;
            }
            _newNodes[i].bounds.position = _newPos;
        }

        return _newNodes;
    }
}

public class Quadtree
{
    public QuadTreeNode root = new QuadTreeNode();
    public int maxObjectsPerNode = 3;
    public int maxDepth = 3;

    public bool Insert(GameObject _nodeObject, QuadTreeNode _location)
    {
        if (!_location.bounds.Contains(_nodeObject.transform.position))
        {
            return false;
        }
        else
        {
            if(_location.children.Length == 0)
            {
                if(_location.objects.Count >= maxObjectsPerNode && _location.currentDepth < maxDepth)
                {
                    _location.children = root.Split(root);
                    _location.objects = null;
                }
                else
                {
                    _location.objects.Add(_nodeObject);
                    return true;
                }
                
            }
            else
            {
                foreach(QuadTreeNode _node in _location.children)
                {
                    if (_node.bounds.Contains(_nodeObject.transform.position))
                    {
                        if(Insert(_nodeObject, _node))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }

    public List<GameObject> Retrive(QuadTreeNode _node = null)
    {
        List<GameObject> _temp = new List<GameObject>();
        
        if(_node == null)
        {
            if (!root.bounds.Contains(_node.bounds.position))
            {
                return _temp;
            }
            else if (root.children.Length > 0)
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
        }
        else
        {
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
        }

        return _temp;
    }
}

