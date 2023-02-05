using UnityEngine;

public class Health : MonoBehaviour
{
    //Health Values
    public int maxHealth = 100;
    public int currentHealth;
    
    

    //Animator
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("isDead", true);
        Debug.Log(gameObject.name + " has died.");
    }
}
