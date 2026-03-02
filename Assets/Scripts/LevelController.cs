using NaughtyAttributes;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private void PlayASound()
    {
        AudioManager.Instance.PlaySound();
    }

#if UNITY_EDITOR
    [Button("Call the AudioManager and ask it to play a sound!")]
    public void PlaySoundTest() => PlayASound();
#endif
}
