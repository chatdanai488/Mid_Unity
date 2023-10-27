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
    public GameObject GunPoint;
    private bool PreviousValue;
    public GameObject BulletPrefab;
    public LayerMask ObstructLayer;
    private BoxCollider2D boxCollider2d;
    private bool isHurt;


    private bool ShootCooldown;
    public GameObject HealthBarObject;
    // Plaayer Attribute

    private float Health;
    private int MaxHealth;
    private float Defense;
    private int MaxDefense;
    private float Attack;
    private float AttackSpeed;
    private float BulletCount;
    // Start is called before the first frame update

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    private void InitializeAttribute()
    {
        MaxHealth = 100;
        Health = 100;
        Defense = 100;
        MaxDefense = 100;
        Attack = 7;
        AttackSpeed = 1 / 1;
        BulletCount = 30;

        ShootCooldown = false;
    }
    private void CheckGrounded()
    {
        float extraHeightText = 0.1f;
        Color rayColor = Color.red;
        Vector2 LeftRayCast = new Vector2(transform.position.x - 1f, transform.position.y);
        Vector2 RightRayCast = new Vector2(transform.position.x + 1f, transform.position.y);
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);


        if (Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, extraHeightText,ObstructLayer))
        {
            isJump = false;
            anim.SetBool("isFall", true);
            
        }
        else
        {
            isJump = true;
        }

    }
    private void Move()
    {
        movex = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movex * speed, rb.velocity.y);

        if (movex > 0.01f)
        {
            spriteRenderer.flipX = false;
            //transform.Rotate(0f,180f,0f);
        }
        else if (movex < -0.01f)
        {
            spriteRenderer.flipX = true;
            //transform.Rotate(0f, 360f, 0f);

        }
    }
    private void Duck()
    {
        if (Input.GetKey(KeyCode.S))
        {

            anim.SetBool("isDuck", true);
            boxCollider2d.size = new Vector2 (boxCollider2d.size.x, 0.2f);
            boxCollider2d.offset = new Vector2(boxCollider2d.offset.x, -0.098f);
        }
        else
        {
            anim.SetBool("isDuck", false);
            boxCollider2d.size = new Vector2(boxCollider2d.size.x, 0.301937f);
            boxCollider2d.offset = new Vector2(boxCollider2d.offset.x, -0.03929493f);



        }
    }
    private void Run()
    {
        if (!isJump)
        {
            anim.SetBool("isRun", movex != 0);
        }

        
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && !isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetBool("isJump", true);
        }
        if (rb.velocity.y < -0.2f)
        {
            anim.SetBool("isJump", false);
        }
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ShootCooldown)
        {
            
            ShootBullet(spriteRenderer.flipX);
            BulletCount -= 1;
            if(BulletCount < 1)
            {
                ReloadBullet();
            }
            
        }


        if (PreviousValue != spriteRenderer.flipX)
        {
            FlipShootPoint(spriteRenderer.flipX);
            PreviousValue = spriteRenderer.flipX;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isShoot", true);
        }
        else
        {
            anim.SetBool("isShoot", false);
        }
    }
    private void ReloadBullet()
    {
        StartCoroutine(AttackCooldown());
        BulletCount = 30;
    }
    private void IsHurt(bool SlimeDirection, float SlimeContact)
    {
        if (SlimeDirection)
        {
            rb.velocity = new Vector2(-1, jump/2);
        }
        else
        {
            rb.velocity = new Vector2(1, jump/2) ;
        }
        
    }
    private IEnumerator InvincibilityFrame()
    {
        isHurt = true;
        anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("isHurt", false);
        isHurt = false;
    }
    private IEnumerator AttackCooldown()
    {
        ShootCooldown = true;
        yield return new WaitForSeconds(AttackSpeed);
        ShootCooldown = false;
    }
    void Start()
    {
        isJump = false;
        rb = GetComponent<Rigidbody2D>();

        GunPoint = GameObject.Find("shoot-point");
        PreviousValue = false;
        InitializeAttribute();
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Move();
        Shoot(); //Shoot-idle
        Run();//Run
        Jump();//Jump
        Duck(); //Duck


        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadBullet();
        }
    }

    private void ShootBullet(bool value)
    {
        
        GameObject Bullet = Instantiate(BulletPrefab, GunPoint.transform.position,Quaternion.identity);
        Bullet.transform.localScale = Vector3.one * 4;
        ShootBullet ShootBulletScript = Bullet.GetComponent<ShootBullet>();
        ShootBulletScript.GetValue(value);
        ShootBulletScript.SetAttribute(Attack);
        
    }
    
    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.CompareTag("Tiles"))
        {
            if (target.contacts[0].normal.y == 1)
            {
                //isJump = false;
                //anim.SetBool("isFall", true);

            }
            
            
        }
        if (target.gameObject.CompareTag("Slime"))
        {
            if (!isHurt)
            {
                SlimeScript SlimeScriptScript = target.gameObject.GetComponent<SlimeScript>();
                float CurrentAttack = SlimeScriptScript.GetAttack();
                bool SlimeDirection = SlimeScriptScript.GetDirection();
                float SlimeContact = target.contacts[0].normal.y;

                Health -= CurrentAttack;

                HealthBar HealthBarScript = HealthBarObject.GetComponent<HealthBar>();
                HealthBarScript.SetBar(Health, MaxHealth);

                if (Health < 0)
                {
                    Destroy(gameObject);
                }
                IsHurt(SlimeDirection, SlimeContact);
                StartCoroutine(InvincibilityFrame());
            }
            
        }
        if (target.gameObject.CompareTag("Lizard"))
        {
            if (!isHurt)
            {
                LizardScript LizardScriptScript = target.gameObject.GetComponent<LizardScript>();
                float CurrentAttack = LizardScriptScript.GetAttack();
                bool SlimeDirection = LizardScriptScript.GetDirection();
                float SlimeContact = target.contacts[0].normal.y;

                Health -= CurrentAttack;

                HealthBar HealthBarScript = HealthBarObject.GetComponent<HealthBar>();
                HealthBarScript.SetBar(Health, MaxHealth);

                if (Health < 0)
                {
                    Destroy(gameObject);
                }
                IsHurt(SlimeDirection, SlimeContact);
                StartCoroutine(InvincibilityFrame());
            }

        }
        if (target.gameObject.CompareTag("LizardBullet"))
        {
            if (!isHurt)
            {
                LizardShootBullet LizardShootBulletScript = target.gameObject.GetComponent<LizardShootBullet>();
                float CurrentAttack = LizardShootBulletScript.GetAttack();
                bool SlimeDirection = false;
                float SlimeContact = target.contacts[0].normal.y;

                Health -= CurrentAttack;

                HealthBar HealthBarScript = HealthBarObject.GetComponent<HealthBar>();
                HealthBarScript.SetBar(Health, MaxHealth);

                if (Health < 0)
                {
                    Destroy(gameObject);
                }
                IsHurt(SlimeDirection, SlimeContact);
                StartCoroutine(InvincibilityFrame());
            }
        }
        if (target.gameObject.CompareTag("Coin"))
        {

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
    private void FlipShootPoint(bool value)
    {
        if (value == true)
        {
            
            GunPoint.transform.position = new Vector3(GunPoint.transform.position.x - 1f, GunPoint.transform.position.y,GunPoint.transform.position.z);
        }
        else if (value == false)
        {
            
            GunPoint.transform.position = new Vector3(GunPoint.transform.position.x + 1f, GunPoint.transform.position.y, GunPoint.transform.position.z);
        }
    }

}
