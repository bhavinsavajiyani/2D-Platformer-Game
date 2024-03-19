using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private ScoreController scoreController;
    private Vector2 boxColliderSize, boxColliderOffset;
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool jumpInput, grounded;

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
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (jumpInput && grounded)
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Force);
        }

        Movement(horizontalInput);
        TriggerAnimations(horizontalInput, jumpInput);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void Movement(float _horizontal)
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
    }

    private void TriggerAnimations(float _horizontal, bool _jumpInput)
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
        if (_jumpInput)
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

    public void KeyCollected()
    {
        Debug.Log("Key Collected...");
        scoreController.IncreaseScore(10);
    }

    public void PlayerKilled()
    {
        Debug.Log("You got Killed...");
        animator.SetBool("died", true);
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        Debug.Log("Restrating the level!");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
