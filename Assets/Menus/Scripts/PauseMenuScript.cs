using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private Button resumeButton;
    private Button controlsScreenBackButton;

    public GameObject pauseMenu;
    public GameObject menuOptions;
    public GameObject controlsScreen;

    public static bool isPaused = false;

    public void Resume() => PauseToggle(true);

    public void ViewControls() => ControlsScreenToggle(true, true);

    public void BackToMainMenu() => SceneManager.LoadScene("MainMenu");

    public void Quit() => Application.Quit();

    public void BackToPauseMenu() => ControlsScreenToggle(false, true);

    private void ControlsScreenToggle(bool isActive, bool isMouseClick)
    {
        if (!isActive && !isMouseClick)
        {
            SimulateMouseClickOnMenuButton(controlsScreenBackButton);
        }

        menuOptions.SetActive(!isActive);
        controlsScreen.SetActive(isActive);
    }

    private void Start()
    {
        resumeButton = menuOptions.GetComponentInChildren<Button>();
        controlsScreenBackButton = controlsScreen.GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controlsScreen.activeSelf)
            {
                ControlsScreenToggle(false, false);
            }
            else
            {
                PauseToggle(false);
            }
        }
    }

    private void PauseToggle(bool isMouseClick)
    {
        Time.timeScale = Convert.ToSingle(isPaused);
        isPaused = !isPaused;

        if (pauseMenu.activeSelf && !isMouseClick)
        {
            SimulateMouseClickOnMenuButton(resumeButton);
        }

        pauseMenu.SetActive(isPaused);
    }

    private void SimulateMouseClickOnMenuButton(Button button)
    {
        // Simulates mouse down on button
        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current),
                                ExecuteEvents.pointerDownHandler);

        // Simulates mouse up on button
        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current),
                                ExecuteEvents.pointerUpHandler);
    }
}