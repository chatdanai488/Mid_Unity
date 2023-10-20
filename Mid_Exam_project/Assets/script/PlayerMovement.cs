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

        GunPoint = GameObject.Find("shoot-point");
        PreviousValue = false;
    }

    // Update is called once per frame
    void Update()
    {
        movex = Input.GetAxis("Horizontal");       
        rb.velocity = new Vector2(movex * speed, rb.velocity.y);
        
        
        //Jump
        if (Input.GetKey(KeyCode.W) && !isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jump);
            anim.SetBool("isJump", true);
        }
        //Duck
        if (Input.GetKey(KeyCode.S))
        {

            anim.SetBool("isDuck", true);
        }
        else
        {
            anim.SetBool("isDuck", false);
        }
        //Flip
        if (movex > 0.01f)
        {
            spriteRenderer.flipX = false;
            //transform.Rotate(0f,180f,0f);
        }
        else if(movex < -0.01f)
        {
            spriteRenderer.flipX = true;
            //transform.Rotate(0f, 360f, 0f);

        }
        //Run
        if (!isJump)
        {
            anim.SetBool("isRun", movex != 0);
        }

        if (rb.velocity.y < -0.2f)
        {
            anim.SetBool("isJump", false);
        }
        //Shoot-idle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            ShootBullet(spriteRenderer.flipX);
            
        }
        

        if(PreviousValue != spriteRenderer.flipX)
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

    private void ShootBullet(bool value)
    {
        
        GameObject Bullet = Instantiate(BulletPrefab, GunPoint.transform.position,Quaternion.identity);
        Bullet.transform.localScale = Vector3.one * 4;
        ShootBullet ShootBulletScript = Bullet.GetComponent<ShootBullet>();
        ShootBulletScript.GetValue(value);

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
    private void FlipShootPoint(bool value)
    {
        if (value == true)
        {
            Debug.Log("Flip True");
            GunPoint.transform.position = new Vector3(GunPoint.transform.position.x - 1f, GunPoint.transform.position.y,GunPoint.transform.position.z);
        }
        else if (value == false)
        {
            Debug.Log("Flip False");
            GunPoint.transform.position = new Vector3(GunPoint.transform.position.x + 1f, GunPoint.transform.position.y, GunPoint.transform.position.z);
        }
    }

}
