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

    public void Play() => SceneManager.LoadScene("TutorialLevel");

    public void SelectLevel(Button button) => MenuScreenToggle(this.selectLevelScreen);

    public void ViewControls(Button button) => MenuScreenToggle(this.controlsScreen);

    public void Quit() => Application.Quit();

    public void BackToMainMenu(Button button)
    {
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
}