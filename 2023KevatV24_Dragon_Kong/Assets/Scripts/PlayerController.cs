using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Rigidbody2D player;
    private float horizontalInput;
    private Vector2 movement;
    public float jumpForce = 5f;
    private bool grounded;
    private Transform ladder;
    private float playerHeight;
    private bool canClimb = false;
    private bool isClimbing = false;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        playerHeight = GetComponent<SpriteRenderer>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //Debug.Log(canClimb);
        Jump();
        movement.y = 0;

        if(canClimb){
            if(verticalInput != 0 && grounded  && horizontalInput == 0){
                isClimbing = true;
            }
        }    
        else{
            isClimbing = false;
        }

        if(horizontalInput != 0){
            isClimbing = false;
        }
        

        if(isClimbing){
            if(player.position.y <= ladder.transform.GetChild(0).transform.position.y + playerHeight/2
            && player.position.y >= ladder.transform.GetChild(1).transform.position.y - 0.1f){
                player.velocity = Vector2.zero;
                player.isKinematic = true;
                movement.y = verticalInput * moveSpeed;
                player.position = new Vector2(ladder.transform.position.x, transform.position.y);
            }else{
                isClimbing = false;
            }
        }else{
            player.isKinematic = false;
        }

        movement.x = horizontalInput * moveSpeed;
    }

    void FixedUpdate(){
        player.position +=  movement * Time.fixedDeltaTime;
    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && grounded == true && !isClimbing){
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = true;
        }
        
        if(collision.gameObject.CompareTag("Ladder")){
            canClimb = true;
            ladder = collision.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = false;
        }

        if(collision.gameObject.CompareTag("Ladder")){
           canClimb = false;
           
        }
    }

}
