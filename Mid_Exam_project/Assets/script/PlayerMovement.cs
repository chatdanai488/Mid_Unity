using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movex;
    private Rigidbody2D rb;
    public float speed;
    public float jump;
    private bool isJump;
    
    // Start is called before the first frame update
    void Start()
    {
        isJump = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movex = Input.GetAxis("Horizontal");       
        rb.velocity = new Vector2(movex * speed, rb.velocity.y);
        


        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jump);

        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.CompareTag("Tiles"))
        {
            isJump = false;
        }
        
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            isJump = true;
        }
        
    }
}
