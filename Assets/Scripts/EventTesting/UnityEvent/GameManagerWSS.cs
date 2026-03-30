using UnityEngine;
using UnityEngine.Events;


public class GameManagerWSS : MonoBehaviour
{
    // Static one shared instance, accessible from anywhere
    public static UnityEvent OnGamePaused = new UnityEvent();
    [SerializeField] private GameObject _objectToSpawn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Invoke any subscriber anywhere in the scene, and it will respond
            Instantiate(_objectToSpawn, new Vector3(0, 2, 0), Quaternion.identity);
            OnGamePaused?.Invoke();
            
        }
    }

}
