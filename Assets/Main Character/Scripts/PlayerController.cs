using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    //ULTIMATE
    
    public bool canUlt = true;
    public bool isUlting = false;
    public float ultTime = 2f;
    public float ultCooldown = 58f;
    [System.Serializable]
    public struct Root
    {
        public Animator rootAnim;
        public BoxCollider2D rootCollider;
    }

    [Header("Root")]
    public Root[] roots;
    //Scale
    [SerializeField]
    private float scaleX = 1f;
    //Movement
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2f;

    //Animator
    private Animator anim;
    public Animator auraAnim;   

    //Attack
    public GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    //Block
    public GameObject blockArea = default;
    private bool blocking = false;
    private float timeToBlock = 0.25f;
    private float blockTimer = 0f;


    //Heal
    private bool canHeal = true;
    private bool isHealing;
    private float healingTime = 2;
    private float healingCooldown = 15f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isHealing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        if (isDashing)
        {
            return;
        }
        if (isUlting)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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
        //Movement
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            anim.SetBool("isMoving", true);
        }
        if (horizontal == 0)
        {
            anim.SetBool("isMoving", false);
        }

        //Movement - Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("isJumping", true);
        }
        if (IsGrounded() && rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && canUlt)
        {
            StartCoroutine(Ultimate());
        }

        //Movement - Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        //Healing (Forest)
        if (Input.GetKeyDown(KeyCode.Alpha3) && canHeal)
        {
            StartCoroutine(Heal());

        }
        //Block
        if (Input.GetKeyDown(KeyCode.Alpha2) && IsGrounded())
        {
            Block();
        }
        if (blocking)
        {
            blockTimer += Time.deltaTime;
            if (blockTimer >= timeToBlock)
            {
                blockTimer = 0;
                blocking = false;
                blockArea.SetActive(blocking);
                anim.SetBool("isBlocking", false);
            }
        }

        //Attacking
        if (Input.GetKeyDown(KeyCode.Alpha1) && IsGrounded())
        {
            Attack();
        }
        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
                anim.SetBool("isMeleeAttacking", false);
            }
        }
        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (isHealing)
        {
            return;
        }
        if(isUlting)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
    private void Attack()
    {
        anim.SetBool("isMeleeAttacking", true);
        attacking = true;
        attackArea.SetActive(attacking);
    }
    private void Block()
    {
        anim.SetBool("isBlocking", true);
        blocking = true;
        blockArea.SetActive(blocking);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        anim.SetBool("isDashing", true);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private IEnumerator Heal()
    {
        canHeal = false;
        isHealing = true;
        anim.SetBool("isHealing", true);
        auraAnim.SetBool("isHealing", true);
        yield return new WaitForSeconds(healingTime);
        isHealing = false;
        anim.SetBool("isHealing", false);
        auraAnim.SetBool("isHealing", false);
        yield return new WaitForSeconds(healingCooldown);
        canHeal = true;

    }
    private IEnumerator Ultimate()
    {
        canUlt = false;
        isUlting = true;
        anim.SetBool("isUlting", true);
        foreach (Root root in roots)
        {
            root.rootAnim.SetBool("isUlting", true);
            root.rootCollider.enabled = true;
        }
        yield return new WaitForSeconds(ultTime);
        isUlting = false;
        anim.SetBool("isUlting", false);
        foreach (Root root in roots)
        {
            root.rootAnim.SetBool("isUlting", false);
            root.rootCollider.enabled = false;
        }
        yield return new WaitForSeconds(ultCooldown);
        canUlt = true;
    }
}