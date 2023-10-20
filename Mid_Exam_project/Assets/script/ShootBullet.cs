using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool Direction;
    private SpriteRenderer spriteRenderer;
    public int bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 10;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Direction)
        {
            rb.velocity = new Vector2(-bulletSpeed, rb.velocity.y);
            spriteRenderer.flipX = true;
        }
        else
        {
            rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
           
        }
        
    }
    public void GetValue(bool Value)
    {
        Direction = Value;
    }


    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            Destroy(gameObject);
        }
        if (target.gameObject.CompareTag("Enemy"))
        {
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }

}
