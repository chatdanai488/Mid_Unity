using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool Direction;
    private SpriteRenderer spriteRenderer;
    public int bulletSpeed;
    private float BulletAttack;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 10;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    public void SetAttribute(float Attack)
    {
        
        BulletAttack = Attack;
    }
    public float GetAttack()
    {
        float AttackMultiplier = Random.Range(-5, 6);
        float CurrentAttack = BulletAttack + BulletAttack*AttackMultiplier/20;
        
        return CurrentAttack;
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

    private IEnumerator Hit()
    {
        anim.SetBool("IsHit", true);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            StartCoroutine(Hit());
        }
        if (target.gameObject.CompareTag("Slime") || target.gameObject.CompareTag("Lizard"))
        {
            StartCoroutine(Hit());
        }

    }

}
