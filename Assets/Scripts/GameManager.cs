using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TextMeshProUGUI _scoreText;

    private string[] _sceneNames = new string[] {"GameCore", "LevelScene", "LevelScene2"};
    public int Score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void LoadScene()
    {
        int i = 0;
        bool _loaded = false;
        string _currentScene = SceneManager.GetActiveScene().name;

        while(_loaded == false)
        {
            Debug.Log(i + _sceneNames[i]);
            if ( (_currentScene == _sceneNames[i]) || _sceneNames[i] == "GameCore")
            {
               i++;
            }
            else if (i >= _sceneNames.Length)
            {
                Debug.Log("No Scene Found");
            }
            else
            {
                Debug.Log(_sceneNames[i] + i);
                Score ++;
                _scoreText.text = "Score: " + Score;
                SceneManager.LoadScene(_sceneNames[i], LoadSceneMode.Single);
                _loaded = true;
            }
        }    
    }
}
