using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Rigidbody2D myRigidbody;

    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other) {
        speed *= -1;
        FlipEmemy();    
    }

    void FlipEmemy() {     
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
}
