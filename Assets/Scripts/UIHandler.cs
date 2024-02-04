using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Button rateBtn;
    [SerializeField] private Button[] levelBtns;

    void Start()
    {
        slider.value = AudioListener.volume;
        if (levelBtns != null)
        {
            for (int i = 0; i < levelBtns.Length; i++)
            {
                if (i < YandexGame.savesData.maxLevel) levelBtns[i].interactable = true;
            }
        }
        if (rateBtn != null && YandexGame.EnvironmentData.reviewCanShow) rateBtn.interactable = true;
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(YandexGame.savesData.maxLevel + 2);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeAudioVolume()
    {
        AudioListener.volume = slider.value;
    }

    public void Rate()
    {
        YandexGame.ReviewShow(true);
    }

    public void Reset()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
