using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFormController : MonoBehaviour
{
    [SerializeField]
    private float scaleX = 1f;
    [SerializeField] private float timeLimit = 29f;
    private float timePassed = 0f;
    //Movement
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2f;
    //Vicious Bite
    public GameObject vBArea = default;
    private bool VB = false;
    private float timeToVB = 0.25f;
    private float vBTimer = 0f;
    //Ferocious Bite
    public GameObject fBArea = default;
    private bool FB = false;
    private float timeToFB = 0.5f;
    private float fBTimer = 0f;


    //Heal
    private bool canHeal = true;
    private bool isHealing;
    private float healingTime = 2;
    private float healingCooldown = 13f;
   
    //Animator
    private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Health health;
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        Transform transform = GetComponent<Transform>();
        Vector3 newScale = transform.localScale;
        newScale.x = scaleX;
        transform.localScale = newScale;
    }
    private void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
        //Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isDashing)
        {
            return;
        }
        if (rb.velocity.x < 0)
        {
            Transform transform = GetComponent<Transform>();
            Vector3 newScale = transform.localScale;
            newScale.x = -1f;
            transform.localScale = newScale;
        }
        if (rb.velocity.x > 0)
        {
            Transform transform = GetComponent<Transform>();
            Vector3 newScale = transform.localScale;
            newScale.x = 1f;
            transform.localScale = newScale;
        }
        if (horizontal != 0)
        {
            anim.SetBool("isRunning", true);
        }
        if (horizontal == 0 && VB == false && FB == false)
        {
            anim.SetBool("isRunning", false);
        }
        //Movement - Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        //Movement - Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        //Attacking
        //Vicious Bite
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ViciousBite();
        }
        if (VB)
        {
            vBTimer += Time.deltaTime;
            if (vBTimer >= timeToVB)
            {
                vBTimer = 0;
                VB = false;
                vBArea.SetActive(VB);
                anim.SetBool("ViciousBite", false);
            }
        }
        //Ferocious Bite
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FerociousBite();
        }
        if (FB)
        {
            fBTimer += Time.deltaTime;
            if (fBTimer >= timeToFB)
            {
                fBTimer = 0;
                FB = false;
                fBArea.SetActive(FB);
                anim.SetBool("FerociousBite", false);
                anim.SetBool("isAnimal", true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)&&canHeal)
        {
            StartCoroutine(Heal());
        }


        Flip();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private void ViciousBite()
    {
        anim.SetBool("isAnimal", false);
        anim.SetBool("ViciousBite", true);
        VB = true;
        vBArea.SetActive(VB);
    }
    private void FerociousBite()
    {
        anim.SetBool("isAnimal", false);
        anim.SetBool("FerociousBite", true);
        FB = true;
        fBArea.SetActive(FB);
    }    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private IEnumerator Heal()
    {
        canHeal = false;
        isHealing = true;
        health.currentHealth += 25;
        yield return new WaitForSeconds(healingTime);
        isHealing = false;
        yield return new WaitForSeconds(healingCooldown);
        canHeal = true;

    }
}
