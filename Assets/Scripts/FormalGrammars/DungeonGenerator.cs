using System.Collections.Generic;
using System.Data;
using NaughtyAttributes;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _entrancePrefab;
    [SerializeField] private GameObject _combatPrefab;
    [SerializeField] private GameObject _puzzlePrefab;
    [SerializeField] private GameObject _treasurePrefab;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private float xOffSetAmount = 5f;

    public enum Nodes {none, Dungeon, Entrance, RoomSequence, Boss, Room, Combat, Puzzle, Treasure};

    public Nodes node;
    Dictionary<Nodes, List<List<Nodes>>> rules;

    Dictionary<Nodes, GameObject> _prefabDic;
    private System.Random _rng = new System.Random();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _prefabDic = new Dictionary<Nodes, GameObject>
        {
            {Nodes.Entrance, _entrancePrefab},
            {Nodes.Combat, _combatPrefab},
            {Nodes.Puzzle, _puzzlePrefab},
            {Nodes.Treasure, _treasurePrefab},
            {Nodes.Boss, _bossPrefab}
        };
        IntialiseGrammer();
        List<Nodes> _dungeon = Expand(Nodes.Dungeon);
        BuildDungeon(_dungeon);
    }
    private List<Nodes> Expand(Nodes _node)
    {
        if (!rules.ContainsKey(_node))
        {
            return new List<Nodes> {_node};
        }

        var _productions = rules[_node];
        var _chosen = _productions[_rng.Next(_productions.Count)];

        List<Nodes> _result = new List<Nodes>();

        foreach(var _sym in _chosen)
        {
            _result.AddRange(Expand(_sym));
        }
        return _result;
    }
    private void IntialiseGrammer()
    {
        rules = new Dictionary<Nodes, List<List<Nodes>>>()
        {
            {Nodes.Dungeon, new List<List<Nodes>> 
                {
                    new List<Nodes>
                    {
                        Nodes.Entrance, Nodes.RoomSequence, Nodes.Boss
                    }
            
                }
            },
            {Nodes.RoomSequence, new List<List<Nodes>> 
                {
                    new List<Nodes>
                    {
                        Nodes.Room
                    },
                    new List<Nodes>
                    {
                        Nodes.Room, Nodes.RoomSequence
                    },
                    new List<Nodes>
                    {
                        Nodes.Room, Nodes.RoomSequence, Nodes.RoomSequence
                    }
            
                }
            },
            {Nodes.Room, new List<List<Nodes>> 
                {
                    new List<Nodes>
                    {
                        Nodes.Combat
                    },
                    new List<Nodes>
                    {
                        Nodes.Puzzle
                    },
                    new List<Nodes>
                    {
                        Nodes.Treasure
                    }
                }
            }
        };
    }
    public void BuildDungeon(List<Nodes> _layout)
    {
        float _xOffSet = 0f;

        foreach(Nodes _room in _layout)
        {
            GameObject _prefab = _prefabDic[_room];

            if(_prefab != null)
            {
                Instantiate(_prefab, new Vector3(_xOffSet, 0, 0), Quaternion.identity);
                _xOffSet += xOffSetAmount;
            }
        }
    }
}
