using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    private Vector2 boxColliderSize, boxColliderOffset;

    private void Awake()
    {
        boxColliderSize = boxCollider2D.size;
        boxColliderOffset = boxCollider2D.offset;
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch Input
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Trigger Run animation
        animator.SetFloat("speed", Mathf.Abs(horizontalInput));

        // Make player face in the direction of input
        Vector3 scale = transform.localScale;
        if(horizontalInput < 0)
        {
            scale.x = Mathf.Abs(transform.localScale.x) * -1.0f;
        }

        else if (horizontalInput > 0)
        {
            scale.x = Mathf.Abs(transform.localScale.x);
        }

        transform.localScale = scale;

        // Trigger Crouch Animation when CTRL key is pressed
        if(Input.GetKey(KeyCode.LeftControl) ||  Input.GetKey(KeyCode.RightControl))
        {
            animator.SetBool("crouch", true);

            Vector2 targetOffset = new Vector2(0.01172504f, 0.5346941f);
            Vector2 targetSize = new Vector2(0.5128101f, 1.245243f);
            boxCollider2D.offset = targetOffset;
            boxCollider2D.size = targetSize;
        }

        else if(Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            animator.SetBool("crouch", false);
        }

        // Trigger Jump Animation when vertical input is received
        if(verticalInput > 0)
        {
            animator.SetBool("jump", true);
        }

        else if(verticalInput <= 0)
        {
            animator.SetBool("jump", false);
        }
    }

    public void Crouch()
    {
        boxCollider2D.offset = boxColliderOffset;
        boxCollider2D.size = boxColliderSize;
    }
}

// offsetX = 0.01172504
// offsetY = 0.5346941
// sizeX = 0.5128101
// sizeY = 1.245243
