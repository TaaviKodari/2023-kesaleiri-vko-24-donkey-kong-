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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalInput);
        Jump();
        movement.x = horizontalInput * moveSpeed;
    }

    void FixedUpdate(){
        player.position +=  movement * Time.fixedDeltaTime;
    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && grounded == true){
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Platform")){
            grounded = false;
        }
    }

}
