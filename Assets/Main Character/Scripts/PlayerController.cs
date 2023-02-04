using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float movementVelocity;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private Animator anim;
    public GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

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
        //Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        movementVelocity = horizontal;


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if(horizontal != 0)
        {
            anim.SetBool("isMoving", true);
        }
        if(horizontal == 0)
        {
            anim.SetBool("isMoving", false);
        }

        Flip();
        //Attacking
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Attack();
        }
        if(attacking)
        {
            timer += Time.deltaTime;
            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
       
    }

    private void FixedUpdate()
    {
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
        attacking = true;
        attackArea.SetActive(attacking);
    }
}