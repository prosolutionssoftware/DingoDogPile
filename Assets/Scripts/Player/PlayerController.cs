using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
	public bool isFacingRight;
    [HideInInspector]
    public bool canJump = false;

    public float maxSpeed = 5f;
    public float moveForceMultiplier = 365f;
    public float jumpForceMultiplier = 1000f;

	public AudioClip[] jumpClips;

    private Transform feet;
    private bool isOnGround = false;

    private Animator animator;

    void Awake()
    {
		isFacingRight = true;
        feet = transform.Find("feet");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isOnGround = Physics2D.Linecast(transform.position, feet.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            canJump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(h));
        if (h * rigidbody2D.velocity.x < maxSpeed)
        {
            rigidbody2D.AddForce(Vector2.right * h * moveForceMultiplier);
        }
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
        }
        if (h > 0 && !isFacingRight)
        {
            Turn();
        }
        else if (h < 0 && isFacingRight)
        {
            Turn();
        }
        if (canJump)
        {
            Jump();
        }

    }

    void Turn()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump()
    {
        animator.SetTrigger("Jump");
        int i = Random.Range(0, jumpClips.Length);
        AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
        rigidbody2D.AddForce(new Vector2(0f, jumpForceMultiplier));
        canJump = false;
    }
}
