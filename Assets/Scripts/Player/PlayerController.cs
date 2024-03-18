using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    private Vector2 boxColliderSize, boxColliderOffset;
    private Rigidbody2D rb;
    private float verticalInput, horizontalInput;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxColliderSize = boxCollider2D.size;
        boxColliderOffset = boxCollider2D.offset;
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch Input
        verticalInput = Input.GetAxisRaw("Jump");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        Movement(horizontalInput, verticalInput);
        TriggerAnimations(horizontalInput, verticalInput);
    }

    private void FixedUpdate()
    {
        if(verticalInput > 0)
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Force);
        }
    }

    private void Movement(float _horizontal, float _vertical)
    {
        // Horizontal Movement
        Vector3 pos = transform.position;
        pos.x += _horizontal * speed * Time.deltaTime;
        transform.position = pos;

        // Make player face in the direction of input
        Vector3 scale = transform.localScale;
        if (_horizontal < 0)
        {
            scale.x = Mathf.Abs(transform.localScale.x) * -1.0f;
        }

        else if (_horizontal > 0)
        {
            scale.x = Mathf.Abs(transform.localScale.x);
        }

        transform.localScale = scale;

        // Vertical Movement (jump)
        
    }

    private void TriggerAnimations(float _horizontal, float _vertical)
    {
        // Trigger Run animation
        animator.SetFloat("speed", Mathf.Abs(_horizontal));

        // Trigger Crouch Animation when CTRL key is pressed
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            animator.SetBool("crouch", true);
            ChangeColliderSizeWhileCrouching();
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            animator.SetBool("crouch", false);
        }

        // Trigger Jump Animation when vertical input is received
        if (_vertical > 0)
        {
            animator.SetBool("jump", true);
        }

        else
        {
            animator.SetBool("jump", false);
        }
    }

    private void ChangeColliderSizeWhileCrouching()
    {
        Vector2 targetOffset = new Vector2(0.01172504f, 0.5346941f);
        Vector2 targetSize = new Vector2(0.5128101f, 1.245243f);
        boxCollider2D.offset = targetOffset;
        boxCollider2D.size = targetSize;
    }

    // Called during Crouch Animation Event
    public void ResetBoxCollider2D()
    {
        boxCollider2D.offset = boxColliderOffset;
        boxCollider2D.size = boxColliderSize;
    }
}
