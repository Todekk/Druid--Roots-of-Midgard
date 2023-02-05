using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public KeyCode keyToPress = KeyCode.E;
    public string sceneName;
    private bool playerInZone;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && playerInZone)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInZone = false;
        }
    }
}
