using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the player movement
    private Rigidbody2D rb;
    public Animator anim;
    private Vector2 movement;
    private bool isInTriggerArea = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        // Check if player is in the trigger area and presses 'E'
        if (isInTriggerArea)
        {
            Debug.Log("Player is in trigger area");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed");
            }
        }
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}