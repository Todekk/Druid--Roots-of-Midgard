using UnityEngine;
using UnityEngine.UI;

public class DisplayBOSSHP : MonoBehaviour
{
    public Health health;
    public Text healthText;

    private void Update()
    {
        healthText.text = "Surtr: "+health.currentHealth;
    }
}
