using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class LSystemGenerator : MonoBehaviour
{
    [SerializeField] private int _interations = 1;
    [SerializeField] private float _f = 10f;
    [SerializeField] private LsystemDict[] _fProductionRules;
    [SerializeField] private LsystemDict[] _gProductionRules;
    [SerializeField] private float _g = 10f;
    [SerializeField] private float _rotation = 10f;
    private enum LsystemDict {none, f, g, plus, minus, repeat, save};
    [SerializeField] private LsystemDict[] axiom;
    [SerializeField] private GameObject _LsystemPoint;
    [SerializeField] private GameObject _connector;

    private GameObject _currentPoint;
    private GameObject _previousPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<LsystemDict> _lsystemlist = LsystemArrayToList(axiom);
        for(int i = 0; i < _interations; i++)
        {
            _lsystemlist = IterateProdutionRules(_lsystemlist);
        }
        CreateLsystem(_lsystemlist, 0);

    }
    private List<LsystemDict> LsystemArrayToList(LsystemDict[] _array)
    {
        List<LsystemDict> _list = new();

        for(int i = 0; i < _array.Length; i++)
        {
            _list.Add(_array[i]);
        }
        return _list;
    }
    private List<LsystemDict> IterateProdutionRules(List<LsystemDict> _lsystemlist)
    {
        List<LsystemDict> _newLsystemlist = new();

        for(int i = 0; i < _lsystemlist.Count; i++)
        {
            LsystemDict _currentSymbol = _lsystemlist[i];
            switch (_currentSymbol)
            {
                case LsystemDict.f:

                    for(int j = 0; j < _fProductionRules.Length; j++)
                    {
                        _newLsystemlist.Add(_fProductionRules[j]);
                    }
                    
                    break;
                case LsystemDict.g:
                    for(int j = 0; j < _gProductionRules.Length; j++)
                    {
                        _newLsystemlist.Add(_gProductionRules[j]);
                    }   

                    break;
                case LsystemDict.minus:
                    //_newLsystemlist.Add(LsystemDict.minus);
                    break;
                case LsystemDict.plus:
                    //_newLsystemlist.Add(LsystemDict.plus);
                    break;
                case LsystemDict.repeat:
                    //_newLsystemlist.Add(LsystemDict.repeat);
                    break;
                case LsystemDict.save:
                    //_newLsystemlist.Add(LsystemDict.save);
                    break;
            }
        }

        return _newLsystemlist;
    }
    private void CreateLsystem(List<LsystemDict> _lsystemlist, int _currentDepth, GameObject _startingPoint = null)
    {
        List<GameObject> _recurList = new();
        
        if (_startingPoint != null)
        {
            _previousPoint = _startingPoint;
        }
        for(int i = 0; i < _lsystemlist.Count; i++)
        {
            LsystemDict _currentSymbol = _lsystemlist[i];
            Vector3 _NewPos;
            Quaternion _newRot;

            switch (_currentSymbol)
            {
                case LsystemDict.f:
                    _NewPos = new Vector3(_f, 0, 0);
                    _currentPoint = Instantiate(_LsystemPoint);
                    if (_previousPoint != null)
                    {
                        _currentPoint.transform.SetParent(_previousPoint.transform);
                    }
                    _currentPoint.transform.localPosition = _NewPos;
                    _currentPoint.transform.localRotation = Quaternion.identity;
                    
                    SpawnConnector(_currentPoint, _previousPoint);
                    _previousPoint = _currentPoint;

                    
                    break;
                case LsystemDict.g:
                    _NewPos = new Vector3(_g, 0, 0);
                    _currentPoint = Instantiate(_LsystemPoint);
                    if (_previousPoint != null)
                    {
                        _currentPoint.transform.SetParent(_previousPoint.transform);
                    }
                    else
                    {
                        _currentPoint.transform.position = _NewPos;
                    }
                    _currentPoint.transform.localPosition = _NewPos;
                    _currentPoint.transform.localRotation = Quaternion.identity;

                    SpawnConnector(_currentPoint, _previousPoint);


                    _currentPoint.transform.position = _NewPos;
                    _previousPoint = _currentPoint;

                    
                        break;
                case LsystemDict.plus:
                    _newRot = Quaternion.Euler(0, _rotation, 0);
                    _previousPoint.transform.localRotation = _newRot;
                    break;
                case LsystemDict.minus:
                    _newRot = Quaternion.Euler(0, -_rotation, 0);
                    _previousPoint.transform.localRotation = _newRot;
                    break;
                case LsystemDict.repeat:
                    if (_currentDepth < _interations)
                    {
                        for(int j = 0; j < _recurList.Count; j++)
                        {
                            CreateLsystem(_lsystemlist, _currentDepth + 1, _recurList[j]);
                            Debug.Log("sad");
                        }
                        
                    }
                    
                    break;
                case LsystemDict.save:
                    _recurList.Add(_previousPoint);
                    break;
            }
        }
    }

    private void SpawnConnector(GameObject _pointOne, GameObject _pointTwo)
    {
        if(_pointTwo == null)
        {
            _pointTwo = new();
        }
        Vector3 _midpoint = Vector3.Lerp(_pointOne.transform.position, _pointTwo.transform.position, 0.5f);
        float _distance = Vector3.Distance(_pointOne.transform.position, _pointTwo.transform.position);
        GameObject _SpawnedConnector = Instantiate(_connector, _midpoint, Quaternion.identity);
        Debug.Log(_distance);
        _SpawnedConnector.transform.localScale = new Vector3(_SpawnedConnector.transform.localScale.x, _SpawnedConnector.transform.localScale.y, _distance);

        _SpawnedConnector.transform.LookAt(_pointOne.transform);
    }

}
