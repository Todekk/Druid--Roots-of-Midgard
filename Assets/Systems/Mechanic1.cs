using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic1 : MonoBehaviour
{
    public string sceneName;
    private bool playerInZone;
    private bool canWave = true;
    private bool isWaving;
    private float waveTime = 1;
    private float wavesCooldown = 20f;
    [System.Serializable]
    public struct Ability
    {
        public GameObject explosion;
    }

    [Header("Wave1")]
    public Ability[] abilities;
    [System.Serializable]
    public struct Attack
    {
        public GameObject explosion;
    }

    [Header("Wave2")]
    public Attack[] attacks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isWaving)
        {
            return;
        }
        if (canWave)
        {
            StartCoroutine(Mechanic());
        }
    }
    private IEnumerator Mechanic()
    {
        canWave = false;
        isWaving = true;
        foreach (Ability ability in abilities)
        {
            ability.explosion.SetActive(true);
        }
        yield return new WaitForSeconds(waveTime);
        foreach (Ability ability in abilities)
        {
            ability.explosion.SetActive(false);
        }
        foreach (Attack attack in attacks)
        {
            attack.explosion.SetActive(true);
        }
        isWaving = false;
        yield return new WaitForSeconds(waveTime);
        foreach (Attack attack in attacks)
        {
            attack.explosion.SetActive(false);
        }
        foreach (Ability ability in abilities)
        {
            ability.explosion.SetActive(true);
        }
        yield return new WaitForSeconds(waveTime);
        foreach (Ability ability in abilities)
        {
            ability.explosion.SetActive(false);
        }
        yield return new WaitForSeconds(wavesCooldown);
        canWave = true;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInZone = true;
            canWave = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInZone = false;
            canWave = false;
        }
    }
}
