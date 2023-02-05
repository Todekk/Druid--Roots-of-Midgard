using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    public Health health;
    public Text healthText;

    private void Update()
    {
        healthText.text = health.currentHealth + "%";
    }
}
