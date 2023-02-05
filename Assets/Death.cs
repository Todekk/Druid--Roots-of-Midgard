using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public Health health;
    public CapsuleCollider2D human;
    public string sceneName = "TutorialLevel";
    public Rigidbody2D character;
    public Animator anim;
    public MonoBehaviour humanScript;
    public MonoBehaviour animalScript;
    public BoxCollider2D animal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth <= 0)
        {
            StartCoroutine(Restart());
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetTrigger("Die");
            character.gravityScale = 0;
            human.enabled = false;
            humanScript.enabled = false;
            animal.enabled = false;
            animalScript.enabled = false;
        }
    }
    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(sceneName);

    }
}
