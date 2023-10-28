using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardShootBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool Direction;
    private SpriteRenderer spriteRenderer;
    public int bulletSpeed;
    private float BulletAttack;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 10;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetAttribute(float Attack)
    {

        BulletAttack = Attack;
    }
    public float GetAttack()
    {
        float AttackMultiplier = Random.Range(-5, 6);
        float CurrentAttack = BulletAttack + BulletAttack * AttackMultiplier / 20;

        return CurrentAttack;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Direction)
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
        Debug.Log(Direction);
    }


    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            Destroy(gameObject);
        }
        if (target.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (target.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(target.gameObject);
        }
        if (target.gameObject.CompareTag("Slime"))
        {
            Destroy(gameObject);
        }
        if (target.gameObject.CompareTag("Lizard"))
        {
            Destroy(gameObject);
        }
        if (target.gameObject.CompareTag("LizardBoss"))
        {
            Destroy(gameObject);
        }
    }

}
