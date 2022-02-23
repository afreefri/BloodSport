using UnityEngine;

/*
 * Goes on player 
 * takes care of player movement and animating the movement
 * note: jump height of player can be adjusted under the player movement script on the player object
 * note: speed of player can be adjusted under the player movement script on the player object
 */


public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float jumpHeight;
    private Rigidbody2D body; // reference to player's rigid body 
    private Animator anim; // reference to player animator
    private bool grounded; 

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //right or left arrow input
        
        //MOVE PLAYER LEFT OR RIGHT
        body.velocity = new Vector2(horizontalInput * playerSpeed, body.velocity.y);


        // CHANGING WHICH WAY PLAYER FACES
        if(horizontalInput > 0.01f) // if moving right
        {
            transform.localScale = Vector3.one; //face right
        }
        else if (horizontalInput < -0.01f) // if moving left
        {
            transform.localScale = new Vector3(-1, 1, 1); //face left
        }

        //JUMP
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        //ANIMATIONS
        anim.SetBool("move", horizontalInput != 0); // player move animation when there is a horizontalInput (arrow keys are pressed)
        anim.SetBool("grounded", grounded);
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        anim.SetTrigger("jump");
        grounded = false;
        
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

}
