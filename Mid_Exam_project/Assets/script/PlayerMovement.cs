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
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    // Start is called before the first frame update

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
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
            anim.SetBool("isJump", true);
        }

        if (movex > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if(movex < -0.01f)
        {
            spriteRenderer.flipX = true;

        }

        if (!isJump)
        {
            anim.SetBool("isRun", movex != 0);
        }

        if (rb.velocity.y < -1f)
        {
            anim.SetBool("isJump", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.CompareTag("Tiles"))
        {
            isJump = false;
            anim.SetBool("isFall", true);

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
