using UnityEngine;

/*
 * Goes on player 
 * takes care of player movement and animating the movement
 * note: play around with "gravity scale" (in player object's rigidbody) to make the player jump higher or lower (smaller gravity number = jump higher)
 * note: speed of player can be adjusted under the player movement script on the player object
 */


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private LayerMask groundLayer; 
    private Rigidbody2D body; // reference to player's rigid body 
    private Animator anim; // reference to player animator
    private BoxCollider2D boxCollider; 

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }

        //ANIMATIONS
        anim.SetBool("move", horizontalInput != 0); // player move animation when there is a horizontalInput (arrow keys are pressed)
        anim.SetBool("grounded", isGrounded());
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, playerSpeed);
        anim.SetTrigger("jump");
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    bool isGrounded() // is the player colliding with the ground?
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null; 
    }

}
