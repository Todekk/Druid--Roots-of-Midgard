using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void SelectLevel()
    {
        // TODO: Go to Select Level Scene or UI
    }

    public void Quit() => Application.Quit();
}
