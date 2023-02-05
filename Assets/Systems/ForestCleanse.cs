using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCleanse : MonoBehaviour
{
    public GameObject corruptForest;
    public GameObject cleansedForest;
    public SpriteRenderer shrine;
    public GameObject grass;
    public GameObject portal;
    private bool playerInZone;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && playerInZone)
        {
            corruptForest.SetActive(false);
            shrine.enabled = false;
            cleansedForest.SetActive(true);
            grass.SetActive(true);
            portal.SetActive(true);
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
