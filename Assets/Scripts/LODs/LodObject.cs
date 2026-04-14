using Unity.VisualScripting;
using UnityEngine;

public class LodObject : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject highDetail;
    [SerializeField] private GameObject midDetail;
    [SerializeField] private GameObject lowDetail;
    [SerializeField] private float _highDistance = 5f;
    [SerializeField] private float _midDistance = 2.5f;
    [SerializeField] private float _lowDistance = 1f;
    private float _distancefromCamera;
    private enum CurrentLod {none, high, mid, low, culled};
    private CurrentLod _lodLevel = CurrentLod.none;

    private void Awake()
    {// Disable All Objects before start
        highDetail.SetActive(false);
        midDetail.SetActive(false);
        lowDetail.SetActive(false);
    }
    void Start()
    {
        
    }

    void Update()
    {   //Get distance from the camera
        _distancefromCamera = Vector3.Distance(_camera.transform.position, transform.position);
        _lodLevel = GetLod(_distancefromCamera);
        EnableLod(_lodLevel);
    }

    private CurrentLod GetLod(float _distance)
    {
        CurrentLod _newLodLevel = CurrentLod.none;

        if (_distance < _lowDistance)
        {
            _newLodLevel = CurrentLod.low;
        }
        else if (_distance > _lowDistance && _distance < _midDistance)
        {
            _newLodLevel = CurrentLod.mid;
        }
        else if (_distance > _midDistance && _distance < _highDistance)
        {
            _newLodLevel = CurrentLod.high;
        }
        else if (_distance > _highDistance)
        {
            _newLodLevel = CurrentLod.culled;
        }

        return _newLodLevel;
    }

    private void EnableLod(CurrentLod _lod)
    {
        switch (_lod)
        {
            case CurrentLod.low:
                        lowDetail.SetActive(true);
                midDetail.SetActive(false);
                highDetail.SetActive(false);
                break;
            case CurrentLod.mid:
                lowDetail.SetActive(false);
                        midDetail.SetActive(true);
                highDetail.SetActive(false);
                break;
                case CurrentLod.high:
                lowDetail.SetActive(false);
                midDetail.SetActive(false);
                        highDetail.SetActive(true);
                break;
            case CurrentLod.culled:
                lowDetail.SetActive(false);
                midDetail.SetActive(false);
                highDetail.SetActive(false);
                break;
        }
    }
}
