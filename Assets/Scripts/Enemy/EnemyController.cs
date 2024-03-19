using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float groundDetectionRadius;
    [SerializeField] private Transform groundDetectionPoint;
    private bool isMovingRight = true;

    // Update is called once per frame
    void Update()
    {
        // Move
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        Patrol();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.PlayerKilled();
        }
    }

    private void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionRadius);
        if (hit.collider == false)
        {
            if (isMovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }

            else
            {
                transform.eulerAngles = Vector3.zero;
                isMovingRight = true;
            }
        }
    }
}
