using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float verticalInput;
    private float horizontalInput;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        
        //Moves player based on wasd input.
        rb.velocity = new Vector2 (speed * horizontalInput, speed * verticalInput);

        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }
    }
}
