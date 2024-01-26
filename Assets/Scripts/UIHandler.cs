using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private Button continueBtn;
    [SerializeField] private Button[] levelBtns;
    [SerializeField] private Button[] spritesBtns;

    void Start()
    {
        Time.timeScale = 1;
        slider.value = AudioListener.volume;

        if (YandexGame.savesData.maxLevel > 1 && continueBtn != null) continueBtn.interactable = true;
        if (levelBtns != null)
        {
            foreach (Button btn in levelBtns)
            {
                if (Int32.Parse(btn.name) <= YandexGame.savesData.maxLevel) btn.interactable = true;
            }
        }
        if (spritesBtns != null)
        {
            foreach (Button btn in spritesBtns)
            {
                if (Int32.Parse(btn.name) * 10 <= YandexGame.savesData.maxLevel) btn.interactable = true;
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(3);
        YandexGame.savesData.maxLevel = 1;
    }

    public void Continue()
    {
        SceneManager.LoadScene(YandexGame.savesData.maxLevel + 2);
    }

    public void Play(int level)
    {
        SceneManager.LoadScene(level + 2);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenLevelsMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenShopMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void Rate()
    {
        Debug.Log("Rate");
    }

    public void ChangeAudioVolume()
    {
        AudioListener.volume = slider.value;
    }

    public void Reset()
    {
        YandexGame.ResetSaveProgress();
    }
}
