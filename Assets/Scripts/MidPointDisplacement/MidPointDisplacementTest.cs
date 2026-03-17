using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class MidPointDisplacementTest : MonoBehaviour
{
    [SerializeField] private GameObject gridPoint;
    [SerializeField] private float _gridSize = 1;
    [SerializeField] private float _maxHeight = 2;
    [SerializeField] private int _GridSquareRoot = 11;
    [SerializeField, Range(100, 0)] private float _randomness = 10;
    private MP_GridPoint[,] _masterArray;
    private MP_GridPoint[,] _cornerArray;

    private enum Direction {none, next, sw, nw, ne, se};

    private MP_GridPoint[,] _cornerArraySw = new MP_GridPoint[2,2];
    private MP_GridPoint[,] _cornerArrayNw = new MP_GridPoint[2,2];
    private MP_GridPoint[,] _cornerArrayNe = new MP_GridPoint[2,2];
    private MP_GridPoint[,] _cornerArraySe = new MP_GridPoint[2,2];

    private int _reoccurringCount;


    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if((_GridSquareRoot % 3) == 0)
        {
        _reoccurringCount = _GridSquareRoot / 3;
        SpawnGrid();
        SetHeight();
        }
        else
        {
            Debug.Log("Grid Square Root Must Be divisible by 3");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnGrid()
    {
        int _count = 0;
        _masterArray = new MP_GridPoint[_GridSquareRoot, _GridSquareRoot];
        Vector3 _spawnPos = gameObject.transform.position;
        for(int col = 0; col < _GridSquareRoot; col++)
        {
            for(int row = 0; row < _GridSquareRoot; row++)
            {
                _spawnPos = new Vector3(row * _gridSize, 0, col * _gridSize); 
                MP_GridPoint _gridPoint = Instantiate(gridPoint, _spawnPos, Quaternion.identity).GetComponent<MP_GridPoint>();
                _count++;
                _masterArray[col, row] = _gridPoint;
                _gridPoint.CurrentPos = new Vector2(col, row);
                _gridPoint.DisplayNumber.text = col.ToString() + "_" + row.ToString();
            }
        }
    }

    private void SetHeight()
    {
        //Get first set of corners
        _cornerArray = new MP_GridPoint[,]
        {
            {_masterArray[0,0], _masterArray[_masterArray.GetLength(0) -1, 0]},
            {_masterArray[0, _masterArray.GetLength(1) -1], _masterArray[_masterArray.GetLength(0) -1, _masterArray.GetLength(1) -1]}
        };

        // Set Height Of Corners
        for(int col = 0; col < _cornerArray.GetLength(0); col++)
        {
            for(int row = 0; row < _cornerArray.GetLength(1); row++)
            {   
                _cornerArray[col, row].UpdateHeight(Random.Range(0, _maxHeight));

            }
        }


    SetHeightChild(_cornerArray);

    }

    private void SetHeightChild(MP_GridPoint[,] __cornerArray)
    {
        Direction _currentDirection = Direction.none;
        bool _layerCompleted = false;


        float _avHeightNorth ;
        float _avHeightEast;
        float _avHeightSouth;
        float _avHeightWest;

        float _avHeightCen;


        Vector2 _midPointNorthPos = new (1,1);
        Vector2 _midPointEastPos;
        Vector2 _midPointSouthPos;
        Vector2 _midPointWestPos;

        Vector2 _midPointPos;


        MP_GridPoint _midPointNorth;
        MP_GridPoint _midPointEast;
        MP_GridPoint _midPointSouth;
        MP_GridPoint _midPointWest;
        
        MP_GridPoint _midPointCenter;
        MP_GridPoint[,] _currentCornerArray = __cornerArray;
        MP_GridPoint[,] _previousCornerArray = _currentCornerArray;

        for(int i = 0; i < 10; i++)
        {
            int j = 0;
            while(j < _reoccurringCount)
            {
                
                switch (_currentDirection)
                {
                    case Direction.sw:
                    _currentCornerArray = _cornerArraySw;
                    break;

                    case Direction.nw:
                    _currentCornerArray = _cornerArrayNw;
                    break;

                    case Direction.ne:
                    _currentCornerArray = _cornerArrayNe;
                    break;

                    case Direction.se:
                    _currentCornerArray = _cornerArraySe;
                    break;
                }
                //MidPoints
                //Get average Height and add some randomness 
                _avHeightNorth = (_currentCornerArray[0, 1].Height + _currentCornerArray[1, 1].Height) / 2;
                _avHeightNorth = Random.Range(_avHeightNorth - (_maxHeight / _randomness), _avHeightNorth + (_maxHeight / _randomness));
                _avHeightEast = (_currentCornerArray[1, 0].Height + _currentCornerArray[1, 1].Height) / 2;
                _avHeightEast = Random.Range(_avHeightEast - (_maxHeight / _randomness), _avHeightEast + (_maxHeight / _randomness));
                _avHeightSouth = (_currentCornerArray[0, 0].Height +_currentCornerArray[0, 1].Height) / 2;
                _avHeightSouth = Random.Range(_avHeightSouth - (_maxHeight / _randomness), _avHeightSouth + (_maxHeight / _randomness));
                _avHeightWest = (_currentCornerArray[0, 0].Height + _currentCornerArray[1, 0].Height) / 2;
                _avHeightWest = Random.Range(_avHeightWest - (_maxHeight / _randomness), _avHeightWest + (_maxHeight / _randomness));
                //Get Midpoints
                _midPointNorthPos = (_currentCornerArray[0, 1].CurrentPos + _currentCornerArray[1, 1].CurrentPos) / 2;
                _midPointEastPos = (_currentCornerArray[1, 0].CurrentPos + _currentCornerArray[1, 1].CurrentPos) / 2;
                _midPointSouthPos = (_currentCornerArray[0, 0].CurrentPos +_currentCornerArray[0, 1].CurrentPos) / 2;
                _midPointWestPos = (_currentCornerArray[0, 0].CurrentPos + _currentCornerArray[1, 0].CurrentPos) / 2;
                //Set MidPoints
                _midPointNorth = _masterArray[Mathf.RoundToInt(_midPointNorthPos.x), Mathf.RoundToInt(_midPointNorthPos.y)];
                _midPointEast = _masterArray[Mathf.RoundToInt(_midPointEastPos.x), Mathf.RoundToInt(_midPointEastPos.y)];
                _midPointSouth = _masterArray[Mathf.RoundToInt(_midPointSouthPos.x), Mathf.RoundToInt(_midPointSouthPos.y)];
                _midPointWest = _masterArray[Mathf.RoundToInt(_midPointWestPos.x), Mathf.RoundToInt(_midPointWestPos.y)];
                //SetHeight
                _midPointNorth.UpdateHeight(_avHeightNorth);
                _midPointEast.UpdateHeight(_avHeightEast);
                _midPointSouth.UpdateHeight(_avHeightSouth);
                _midPointWest.UpdateHeight(_avHeightWest);
                _midPointNorth.DisplayNumber.text = "East";
                _midPointEast.DisplayNumber.text = "North";
                _midPointSouth.DisplayNumber.text = "West";
                _midPointWest.DisplayNumber.text = "South";
                //CentrePoint  
                //Get average Height
                _avHeightCen = (_midPointNorth.Height + _midPointEast.Height + _midPointSouth.Height + _midPointWest.Height) / 4;
                //Get CentrePoint
                _midPointPos = (_midPointNorth.CurrentPos + _midPointEast.CurrentPos + _midPointSouth.CurrentPos + _midPointWest.CurrentPos) / 4;
                //Set CentrePoint 
                _midPointCenter = _masterArray[Mathf.RoundToInt(_midPointPos.x), Mathf.RoundToInt(_midPointPos.y)];
                //SetHeight
                _midPointCenter.UpdateHeight(_avHeightCen);

                switch (_currentDirection)
                {
                    case Direction.none:

                        _cornerArraySw = new MP_GridPoint[,]
                        {//working
                            {_currentCornerArray[0, 0], _midPointSouth,},
                            {_midPointWest, _midPointCenter} 
                        };

                        _cornerArrayNw = new MP_GridPoint[,]
                        {
                            {_midPointSouth, _midPointCenter},
                            {_currentCornerArray[0, 1], _midPointNorth}
                        };

                        _cornerArrayNe = new MP_GridPoint[,]
                        { //working
                            {_midPointCenter, _midPointEast},
                            {_midPointNorth, _currentCornerArray[1, 1]}
                        };

                        _cornerArraySe = new MP_GridPoint[,]
                        {
                            {_midPointWest, _currentCornerArray[1, 0]},
                            {_midPointCenter, _midPointEast}
                        };
                        _currentDirection = Direction.sw;

                    break;

                    case Direction.sw:
                        _currentDirection = Direction.nw;

                    break;

                    case Direction.nw:
                        _currentDirection = Direction.ne;
                    break;

                    case Direction.ne:
                        _currentDirection = Direction.se;
                    break;

                    case Direction.se: // this was used to go down a layer but it could only do the bottom left corners
                        // _currentDirection = Direction.sw;
                        // _cornerArraySw = new MP_GridPoint[,]
                        // {//working
                        //     {_currentCornerArray[0, 0], _midPointSouth,},
                        //     {_midPointWest, _midPointCenter} 
                        // };

                        //  _cornerArrayNw = new MP_GridPoint[,]
                        // {
                        //     {_midPointSouth, _midPointCenter},
                        //     {_currentCornerArray[0, 1], _midPointNorth}
                        // };

                        // _cornerArrayNe = new MP_GridPoint[,]
                        // { //working
                        //     {_midPointCenter, _midPointEast},
                        //     {_midPointNorth, _currentCornerArray[1, 1]}
                        // };

                        // _cornerArraySe = new MP_GridPoint[,]
                        // {
                        //     {_midPointWest, _currentCornerArray[1, 0]},
                        //     {_midPointCenter, _midPointEast}
                        // };
                        Debug.Log(j);
                        j++;
                    break;

                }
            }
            
        
        }
    }

}

