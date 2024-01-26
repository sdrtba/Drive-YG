using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject inGameCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
        inGameCanvas.SetActive(!inGameCanvas.activeSelf);
    }
}
