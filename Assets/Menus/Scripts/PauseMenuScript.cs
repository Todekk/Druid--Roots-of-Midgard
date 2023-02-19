using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isPaused = false;

    public static LinkedList<Button> pauseMenuButtons = new LinkedList<Button>();
    public static LinkedListNode<Button> selectedButtonNode;

    public Button resumeButton;
    public Button controlsButton;
    public Button quitButton;
    public Button controlsScreenBackButton;

    public GameObject pauseMenu;
    public GameObject menuOptions;
    public GameObject controlsScreen;

    public void ViewControls() => ControlsScreenToggle(true);

    public void BackToMainMenu() => SceneManager.LoadScene("MainMenu");

    public void Quit() => Application.Quit();

    public void BackToPauseMenu() => ControlsScreenToggle(false);

    private void ControlsScreenToggle(bool isActive)
    {
        menuOptions.SetActive(!isActive);
        controlsScreen.SetActive(isActive);

        if (this.menuOptions.activeSelf)
        {
            MenuButtonScript.selectedButton = selectedButtonNode.Value;
            MenuButtonScript.HoverSelectedButton();
        }
    }

    private void Start()
    {
        pauseMenuButtons.AddLast(resumeButton);
        pauseMenuButtons.AddLast(controlsButton);
        pauseMenuButtons.AddLast(quitButton);

        selectedButtonNode = pauseMenuButtons.First;
        MenuButtonScript.selectedButton = selectedButtonNode.Value;
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.pauseMenu.activeSelf)
        {
            if (this.menuOptions.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    MenuButtonScript.UnhoverSelectedButton();
                    PauseToggle();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    this.SelectNextButton();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    this.SelectPreviousButton();
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    this.ClickSelectedButton();
                }
            }
            else if (this.controlsScreen.activeSelf)
            {
                if (MenuButtonScript.selectedButton != controlsScreenBackButton)
                {
                    MenuButtonScript.selectedButton = controlsScreenBackButton;
                    MenuButtonScript.HoverSelectedButton();

                    return;
                }
                
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
                {
                    this.ClickSelectedButton();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseToggle();
            }
        }
    }

    public void PauseToggle()
    {
        Time.timeScale = Convert.ToSingle(isPaused);
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);

        if (pauseMenu.activeSelf)
        {
            selectedButtonNode = pauseMenuButtons.First;
            MenuButtonScript.selectedButton = selectedButtonNode.Value;
            MenuButtonScript.HoverSelectedButton();
        }
    }

    private void SelectNextButton() =>
        MenuButtonScript.SelectNextButton(ref selectedButtonNode, pauseMenuButtons);

    private void SelectPreviousButton() =>
        MenuButtonScript.SelectPreviousButton(ref selectedButtonNode, pauseMenuButtons);

    private void ClickSelectedButton()
    {
        MenuButtonScript.SimulateMouseClickOnMenuButton(MenuButtonScript.selectedButton);

        switch (MenuButtonScript.selectedButton.name)
        {
            case "Resume Button":
                this.PauseToggle();
                break;

            case "Controls Button":
                this.ViewControls();
                break;

            case "Quit Button":
                this.Quit();
                break;

            case "Back Button":
                this.BackToPauseMenu();
                break;
        }
    }
}