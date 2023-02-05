using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject selectLevelScreen;
    public GameObject controlsScreen;
    public bool isInMainMenu = true;

    public AudioSource buttonClickedSound;

    public void Play()
    {
        buttonClickedSound.Play();
        SceneManager.LoadScene("TutorialLevel");
    }

    public void SelectLevel(Button button)
    {
        buttonClickedSound.Play();
        UnhighlightButtonText(button, "Select Level Text");
        MenuScreenToggle(this.selectLevelScreen);
    }

    public void ViewControls(Button button)
    {
        buttonClickedSound.Play();
        UnhighlightButtonText(button, "Controls Text");
        MenuScreenToggle(this.controlsScreen);
    }

    public void Quit()
    {
        buttonClickedSound.Play();
        Application.Quit();
    } 

    public void BackToMainMenu(Button button)
    {
        buttonClickedSound.Play();
        UnhighlightButtonText(button, "Back Text");

        if (selectLevelScreen.activeSelf)
        {
            MenuScreenToggle(this.selectLevelScreen);
        }
        else if (controlsScreen.activeSelf)
        {
            MenuScreenToggle(this.controlsScreen);
        }
    }

    public void GoToLevel(Button button)
    {
        buttonClickedSound.Play();

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
        isInMainMenu = !isInMainMenu;
        mainMenuScreen.SetActive(isInMainMenu);
        screen.SetActive(!isInMainMenu);
    }

    private void UnhighlightButtonText(Button button, string buttonText)
    {
        TextMeshProUGUI text = button.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(o => o.name == buttonText);
        text.color = Color.white;
    }
}