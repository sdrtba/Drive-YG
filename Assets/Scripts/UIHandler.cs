using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject canvas;
    private bool _isPause = false;
    private int maxLevel;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Start()
    {
        Time.timeScale = 1;
        slider.value = AudioListener.volume;
    }

    void GetLoad()
    {
        maxLevel = YandexGame.savesData.maxLevel;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(maxLevel + 1);
    }

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
        if (_isPause) Time.timeScale = 1;
        else Time.timeScale = 0;
        canvas.SetActive(!canvas.activeSelf);
        _isPause = !_isPause;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex >= 2)
        {
            Pause();
        }
    }
}
