using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
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
            return;
        }
        if (isDashing)
        {
            return;
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
        yield return new WaitForSeconds(healingTime);
        isHealing = false;
        anim.SetBool("isHealing", false);
        yield return new WaitForSeconds(healingCooldown);
        canHeal = true;

    }
}