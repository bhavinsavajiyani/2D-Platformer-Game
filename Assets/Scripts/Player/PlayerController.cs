using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(speed));

        Vector3 scale = transform.localScale;
        if(speed < 0)
        {
            scale.x = Mathf.Abs(transform.localScale.x) * -1.0f;
        }

        else if (speed > 0)
        {
            scale.x = Mathf.Abs(transform.localScale.x);
        }

        transform.localScale = scale;
    }
}
