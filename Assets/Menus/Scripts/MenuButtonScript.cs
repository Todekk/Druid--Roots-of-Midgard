using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    public static Button selectedButton;

    public TextMeshProUGUI text;
    public AudioSource clickSound;
    public AudioSource hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        string buttonParentName = this.transform.parent.name;

        switch (buttonParentName)
        {
            case "Menu Buttons":
                bool isInMainMenuScene = SceneManager.GetActiveScene().name == "MainMenu";
                if (isInMainMenuScene)
                {
                    SelectButtonInMenu(MainMenuScript.mainMenuButtons, ref MainMenuScript.selectedMainMenuButtonNode);
                }
                else
                {
                    SelectButtonInMenu(PauseMenuScript.pauseMenuButtons, ref PauseMenuScript.selectedButtonNode);
                }
                break;

            case "Levels Grid":
                UnhoverAllButtons(MainMenuScript.levelButtons);
                UnhoverSelectedButton();

                if (MainMenuScript.isOnBackButtonInSelectLevelScreen)
                {
                    MainMenuScript.isOnBackButtonInSelectLevelScreen = false;
                }

                MainMenuScript.selectedLevelButtonNode = MainMenuScript.levelButtons.Find(this.GetComponent<Button>());
                selectedButton = MainMenuScript.selectedLevelButtonNode.Value;
                break;

            case "Select Level":
                UnhoverAllButtons(MainMenuScript.levelButtons);
                selectedButton = MainMenuScript.selectLevelScreenBackButtonStatic;
                MainMenuScript.isOnBackButtonInSelectLevelScreen = true;
                break;
        }

        if (text is not null)
        {
            this.text.color = new Color(66f / 255f, 69f / 255f, 33f / 255f);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        }

        this.hoverSound.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (text is not null)
        {
            text.color = Color.black;
        }

        this.clickSound.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.text is not null)
        {
            this.SetDefaultMenuButtonTextColor();
        }
    }

    public static void SimulateMouseClickOnMenuButton(Button button)
    {
        // Simulates mouse down on button
        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current),
                                ExecuteEvents.pointerDownHandler);

        // Simulates mouse up on button
        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current),
                                ExecuteEvents.pointerUpHandler);
    }

    public static void SelectNextButton(ref LinkedListNode<Button> buttonNode, LinkedList<Button> menuButtons)
    {
        UnhoverButton(selectedButton);

        if (buttonNode.Next is not null)
        {
            buttonNode = buttonNode.Next;
        }
        else
        {
            buttonNode = menuButtons.First;
        }

        selectedButton = buttonNode.Value;
        HoverButton(selectedButton);
    }

    public static void SelectPreviousButton(ref LinkedListNode<Button> buttonNode, LinkedList<Button> menuButtons)
    {
        Button button = buttonNode.Value;
        UnhoverButton(button);

        if (buttonNode.Previous is not null)
        {
            buttonNode = buttonNode.Previous;
        }
        else
        {
            buttonNode = menuButtons.Last;
        }

        selectedButton = buttonNode.Value;
        HoverButton(selectedButton);
    }

    public static void HoverButton(Button button)
    {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<MenuButtonScript>().text;

        if (buttonText is not null)
        {
            buttonText.color = new Color(66f / 255f, 69f / 255f, 33f / 255f);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        }

        if (!selectedButton.GetComponent<MenuButtonScript>().clickSound.isPlaying)
        {
            AudioSource buttonHoverSound = button.GetComponent<MenuButtonScript>().hoverSound;
            buttonHoverSound.Play();
        }
    }

    public static void UnhoverButton(Button button)
    {
        MenuButtonScript buttonMenuButtonScript = button.GetComponentInChildren<MenuButtonScript>();
        TextMeshProUGUI buttonText = buttonMenuButtonScript.text;

        if (buttonText is not null)
        {
            buttonMenuButtonScript.SetDefaultMenuButtonTextColor();
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public static void UnhoverAllButtons(LinkedList<Button> menuButtons)
    {
        foreach (Button button in menuButtons)
        {
            UnhoverButton(button);
        }
    }

    public static void HoverSelectedButton() => HoverButton(selectedButton);

    public static void UnhoverSelectedButton()
    {
        UnhoverButton(selectedButton);
    }

    private void SetDefaultMenuButtonTextColor() => this.text.color = new Color(235f / 255f, 235f / 255f, 235f / 255f);

    private static void StopButtonToBeUnhoveredSounds()
    {
        MenuButtonScript selectedButtonMenuButtonScript = selectedButton.GetComponent<MenuButtonScript>();
        AudioSource selectedButtonHoverSound = selectedButtonMenuButtonScript.hoverSound;
        AudioSource selectedButtonClickSound = selectedButtonMenuButtonScript.clickSound;

        if (selectedButtonHoverSound.isPlaying)
        {
            selectedButtonHoverSound.Stop();
        }

        if (selectedButtonClickSound.isPlaying)
        {
            selectedButtonClickSound.Stop();
        }
    }

    private void SelectButtonInMenu(LinkedList<Button> menuButtons, ref LinkedListNode<Button> selectedButtonNode)
    {
        UnhoverAllButtons(menuButtons);
        selectedButtonNode = menuButtons.Find(this.GetComponent<Button>());
        selectedButton = selectedButtonNode.Value;
    }
}