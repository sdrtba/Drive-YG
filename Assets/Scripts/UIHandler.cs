using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private int _maxLevel;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void GetLoad()
    {
        _maxLevel = YandexGame.savesData.maxLevel;
    }

    void Start()
    {
        Time.timeScale = 1;
        slider.value = AudioListener.volume;
        GetLoad();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(2);
        YandexGame.ResetSaveProgress();
    }

    public void Continue()
    {
        GetLoad();
        SceneManager.LoadScene(_maxLevel + 1);
    }

    public void Play(int level)
    {
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
}
