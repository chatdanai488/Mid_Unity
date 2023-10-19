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

    // Plaayer Attribute

    private int Health;
    private int MaxHealth;
    private int Defense;
    private int MaxDefense;
    private int Attack;
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
        


        if (Input.GetKey(KeyCode.W) && !isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jump);
            anim.SetBool("isJump", true);
        }

        if (Input.GetKey(KeyCode.S))
        {

            anim.SetBool("isDuck", true);
        }
        else
        {
            anim.SetBool("isDuck", false);
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

        if (rb.velocity.y < -0.2f)
        {
            anim.SetBool("isJump", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.CompareTag("Tiles"))
        {
            if (target.contacts[0].normal.y == 1)
            {
                isJump = false;
                anim.SetBool("isFall", true);

            }
            
            
        }
        if (target.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            isJump = true;
            anim.SetBool("isFall", false);
        }
        
    }

    
}
