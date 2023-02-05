using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChange : MonoBehaviour
{
    private bool playerInZone;
    private bool canWave = true;
    private bool isWaving;
    private float waveTime = 150f;
    private float wavesCooldown = 50000f;
    public Health health;
    public GameObject bossEnemy;
    public GameObject finalPhase;
    [System.Serializable]
    public struct Ability
    {
        public BoxCollider2D phase1;
        public SpriteRenderer phase1Sprite;
    }

    [Header("Phase")]
    public Ability[] abilities;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            finalPhase.SetActive(false);
            bossEnemy.SetActive(false);
        }
        if (isWaving)
        {
            return;
        }
        if (canWave)
        {
            StartCoroutine(Phase());
        }
        
    }
    private IEnumerator Phase()
    {
        canWave = false;
        isWaving = true;
        yield return new WaitForSeconds(waveTime);
        foreach (Ability ability in abilities)
        {
            ability.phase1.enabled = false;
            ability.phase1Sprite.enabled = false;
            yield return new WaitForSeconds(30);
        }
        isWaving = false;
        yield return new WaitForSeconds(wavesCooldown);
        canWave = true;
    }
}
