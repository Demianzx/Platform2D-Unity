using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScale;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    bool isAlive = true;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();

        gravityScale = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        if(!isAlive) {
            myRigidbody.gravityScale = gravityScale;
            return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value){
        if(!isAlive) {return;}
        myAnimator.SetBool("Fire", true);
        Instantiate(bullet, gun.position, transform.rotation);

    }

    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) {return;}
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {return;}
        
        if (value.isPressed)
        {
            myRigidbody.velocity += Vector2.up * jumpForce;
        }
    }

    void Die(){
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 10;            
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void ClimbLadder()
    {
        
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = gravityScale;
            return;
        }
        myRigidbody.gravityScale = 0f;
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveInput.y * speed);
        myAnimator.SetBool("isClimbing", Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon);
    }

    void Run()
    {
       
            myRigidbody.velocity = new Vector2(moveInput.x * speed, myRigidbody.velocity.y);
            myAnimator.SetBool("isRunning", Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
    }

    void FlipSprite(){      

        if(Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon){
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

        
    }
}
