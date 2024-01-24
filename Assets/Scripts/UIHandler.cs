using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject canvas;
    private bool isPause = false;

    public void Play(int level)
    {
        Debug.Log("Ads");
        SceneManager.LoadScene(level + 1);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);

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

    public void Pause()
    {
        if (isPause) Time.timeScale = 1;
        else Time.timeScale = 0;
        canvas.SetActive(!canvas.activeSelf);
        isPause = !isPause;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex >= 2)
        {
            Pause();
        }
    }
}
