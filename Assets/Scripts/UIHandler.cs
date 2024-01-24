using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private int curLevel;

    public void Play()
    {
        Debug.Log("Ads");
        SceneManager.LoadScene(curLevel + 2);
    }

    public void OpenLevelsMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Rate()
    {
        Debug.Log("Rate");
    }

    public void ChangeAudioVolume()
    {
        AudioListener.volume = slider.value;
    }
}
