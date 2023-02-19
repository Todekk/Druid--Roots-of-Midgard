using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    private bool hasOpenedSelectLevelScreen = false;

    public static Button selectLevelScreenBackButtonStatic;
    public static LinkedList<Button> mainMenuButtons;

    public static LinkedList<Button> levelButtons;

    public static LinkedListNode<Button> selectedMainMenuButtonNode;
    public static LinkedListNode<Button> selectedLevelButtonNode;

    public static bool isOnBackButtonInSelectLevelScreen = false;

    public Button playButton;
    public Button selectLevelButton;
    public Button controlsButton;
    public Button quitButton;
    public Button selectLevelScreenBackButton;
    public Button controlsScreenBackButton;

    public Button tutorialLevelButton;
    public Button levelOneButton;
    public Button levelTwoButton;

    public GameObject mainMenuScreen;
    public GameObject selectLevelScreen;
    public GameObject controlsScreen;

    public bool isInMainMenu = true;

    public void Play() => SceneManager.LoadScene("TutorialLevel");

    public void SelectLevel() => this.MenuScreenToggle(this.selectLevelScreen);

    public void ViewControls() => this.MenuScreenToggle(this.controlsScreen);

    public void Quit() => Application.Quit();

    public void BackToMainMenu()
    {
        if (this.selectLevelScreen.activeSelf)
        {
            this.hasOpenedSelectLevelScreen = false;
            this.MenuScreenToggle(this.selectLevelScreen);
        }
        else if (controlsScreen.activeSelf)
        {
            this.MenuScreenToggle(this.controlsScreen);
        }
    }

    public void GoToLevel(Button button)
    {
        string buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;

        string levelSceneName = null;

        switch (buttonText)
        {
            case "Tutorial":
                levelSceneName = "TutorialLevel";
                break;

            case "Level 1":
                levelSceneName = "LevelOne";
                break;

            case "Level 2":
                // Here should be the scene name of Level 2
                levelSceneName = "LevelTwo";
                break;
        }

        // Check if button text is binded with any of the scene names
        if (levelSceneName is not null)
        {
            SceneManager.LoadScene(levelSceneName);
        }
        else
        {
            Debug.Log("Scene with such name doesn't exist'or should be changed in code!");
        }
    }

    private void MenuScreenToggle(GameObject screen)
    {
        this.isInMainMenu = !this.isInMainMenu;
        this.mainMenuScreen.SetActive(this.isInMainMenu);
        screen.SetActive(!this.isInMainMenu);

        if (this.isInMainMenu)
        {
            MenuButtonScript.selectedButton = selectedMainMenuButtonNode.Value;
            MenuButtonScript.HoverSelectedButton();
        }
    }

    private void Awake()
    {
        mainMenuButtons = new LinkedList<Button>();
        mainMenuButtons.AddLast(this.playButton);
        mainMenuButtons.AddLast(this.selectLevelButton);
        mainMenuButtons.AddLast(this.controlsButton);
        mainMenuButtons.AddLast(this.quitButton);
        selectedMainMenuButtonNode = mainMenuButtons.First;
        
        MenuButtonScript.selectedButton = selectedMainMenuButtonNode.Value;
        MenuButtonScript.HoverSelectedButton();

        levelButtons = new LinkedList<Button>();
        levelButtons.AddLast(this.tutorialLevelButton);
        levelButtons.AddLast(this.levelOneButton);
        levelButtons.AddLast(this.levelTwoButton);
        selectLevelScreenBackButtonStatic = selectLevelScreenBackButton;
    }

    private void Update()
    {
        if (this.mainMenuScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.SelectNextButton();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.SelectPreviousButton();
            }
            else if (Input.GetKeyDown(KeyCode.Return)) // Return = Enter
            {
                this.ClickSelectedButton();
            }
        }
        else if (this.selectLevelScreen.activeSelf)
        {
            if (!this.hasOpenedSelectLevelScreen)
            {
                this.hasOpenedSelectLevelScreen = true;
                selectedLevelButtonNode = levelButtons.First;
                MenuButtonScript.selectedButton = selectedLevelButtonNode.Value;
                MenuButtonScript.HoverSelectedButton();

                return;
            }

            if (!isOnBackButtonInSelectLevelScreen && Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.SelectNextLevelButton();
            }
            else if (!isOnBackButtonInSelectLevelScreen && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.SelectPreviousLevelButton();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MenuButtonScript.UnhoverSelectedButton();

                if (MenuButtonScript.selectedButton != selectLevelScreenBackButtonStatic)
                {
                    MenuButtonScript.selectedButton = selectLevelScreenBackButtonStatic;
                    isOnBackButtonInSelectLevelScreen = true;
                }
                else
                {
                    MenuButtonScript.selectedButton = selectedLevelButtonNode.Value;
                    isOnBackButtonInSelectLevelScreen = false;
                }

                MenuButtonScript.HoverSelectedButton();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuButtonScript.selectedButton = selectLevelScreenBackButtonStatic;
                this.ClickSelectedButton();
                MenuButtonScript.UnhoverSelectedButton();

                MenuButtonScript.selectedButton = selectedMainMenuButtonNode.Value;
                MenuButtonScript.HoverSelectedButton();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                this.ClickSelectedButton();

                if (!isOnBackButtonInSelectLevelScreen)
                {
                    this.GoToLevel(MenuButtonScript.selectedButton);
                }
                else
                {
                    isOnBackButtonInSelectLevelScreen = false;
                    MenuButtonScript.selectedButton = selectedMainMenuButtonNode.Value;
                }
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

                MenuButtonScript.selectedButton = selectedMainMenuButtonNode.Value;
            }
        }
    }

    private void SelectNextButton() => 
        MenuButtonScript.SelectNextButton(ref selectedMainMenuButtonNode, mainMenuButtons);

    private void SelectPreviousButton() => 
        MenuButtonScript.SelectPreviousButton(ref selectedMainMenuButtonNode, mainMenuButtons);

    private void ClickSelectedButton()
    {
        MenuButtonScript.SimulateMouseClickOnMenuButton(MenuButtonScript.selectedButton);

        switch (MenuButtonScript.selectedButton.name)
        {
            case "Play Button":
                this.Play();
                break;

            case "Select Level Button":
                this.SelectLevel();
                break;

            case "Controls Button":
                this.ViewControls();
                break;

            case "Quit Button":
                this.Quit();
                break;

            case "Back Button":
                this.BackToMainMenu();
                break;
        }
    }

    private void SelectNextLevelButton() => 
        MenuButtonScript.SelectNextButton(ref selectedLevelButtonNode, levelButtons);

    private void SelectPreviousLevelButton() => 
        MenuButtonScript.SelectPreviousButton(ref selectedLevelButtonNode, levelButtons);
}