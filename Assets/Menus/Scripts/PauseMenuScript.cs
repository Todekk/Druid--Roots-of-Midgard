using System;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject menuOptions;
    public GameObject controlsScreen;

    public static bool isPaused = false;

    public void Resume() => PauseToggle();

    public void ViewControls() => ControlsScreenToggle(true);

    public void BackToMainMenu() => SceneManager.LoadScene("MainMenu");

    public void Quit() => Application.Quit();

    public void BackToPauseMenu() => ControlsScreenToggle(false);

    private void ControlsScreenToggle(bool isActive)
    {
        menuOptions.SetActive(!isActive);
        controlsScreen.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    private void PauseToggle()
    {
        Time.timeScale = Convert.ToSingle(isPaused);
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
    }
}
