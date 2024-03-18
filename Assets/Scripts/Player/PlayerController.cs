using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

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
}
