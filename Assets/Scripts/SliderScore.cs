using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class SliderScore : MonoBehaviour
{

    [SerializeField] private float _maxScore = 10; 
    private Slider _slider;
    void Start()
    { //Set max score
        _slider = GetComponent<Slider>();
        _slider.maxValue = _maxScore;
    }
    public void UpdateCurrentScore()
    {
        _slider.value = GameManager.Instance.Score;
    }

}
