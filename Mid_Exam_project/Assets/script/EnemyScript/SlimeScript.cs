using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class SlimeScript : MonoBehaviour
{

    // slime attribute
    private float Health;
    private float Defense;
    private float Attack;
    private float AttackSpeed;
    private int MaxHealth;
    private int MaxDefense;
    private float Speed;

    // Raycast
    private float FieldOfView;
    public LayerMask TargetLayer;
    public LayerMask ObstructLayer;
    private GameObject PlayerRef;
    public bool cansee { get; private set; }
    private bool Direction;

    private BoxCollider2D boxCollider2d;

    // Movement
    private float JumpPower;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D SlimeRB;
    private bool IsJump;

    // HealthBar
    public GameObject HealthBarObject;
    private HealthBar HealthBarScript;
    private void InitializeComponent()
    {
        SlimeRB = GetComponent<Rigidbody2D>();
        IsJump = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerRef = GameObject.Find("Player");
        boxCollider2d = GetComponent<BoxCollider2D>();
        IsWaiting = false;

        
    }
    private void InitializeAttribute(int level)
    {


        MaxHealth = 20 + (10 * level);
        MaxDefense = 3 + (1 * level);
        Attack = 2 + (float)(0.5 * level);
        Health = MaxHealth;
        Defense = MaxDefense;
        Speed = 1f;

        FieldOfView = 5f;

        JumpPower = 5.5f;
    }
    private IEnumerator FovCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }


    private bool IsWaiting;


    private void FOV()
    {
        Collider2D[] RangeCheck = Physics2D.OverlapCircleAll(transform.position, FieldOfView, TargetLayer);
        if (RangeCheck.Length > 0)
        {
            Transform Target = RangeCheck[0].transform;
            Vector2 TargetDirection = (Target.position - transform.position).normalized;
            float TargetDistance = Vector2.Distance(transform.position, Target.position);
            
            if (TargetDirection.x > 0)
            {
                Direction = true;
            }
            else
            {
                Direction = false;
            }
            
            if (!Physics2D.Raycast(transform.position, TargetDirection, TargetDistance, ObstructLayer))
            {
                cansee = true;
                
            }

        }
        else if (cansee)
        {
            cansee = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, FieldOfView);

        if (cansee)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, PlayerRef.transform.position);
        }
        Gizmos.color = Color.red;
        
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


        if (Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, ObstructLayer))
        {
            IsJump = false;
            
        }
        else
        {
            IsJump = true;
        }

    }
    private void Jump()
    {
        if (!IsJump && !IsWaiting)
        {
            int Jump = Random.Range(0, 100);
            if (Jump < 30)
            {
                Move();
            }
        }
        else if (!IsJump && cansee)
        {
            Move();
        }
        StartCoroutine(Waiting());
    }
    private void Move()
    {
        
        if (!cansee)
        {
            int LeftOrRight = Random.Range(-1, 2);
            if (LeftOrRight < 0)
            {
                SlimeRB.velocity = new Vector2(Speed, JumpPower);
                spriteRenderer.flipX = true;
            }
            else if (LeftOrRight > 0)
            {
                SlimeRB.velocity = new Vector2(Speed * -1, JumpPower);
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (Direction)
            {
                SlimeRB.velocity = new Vector2(Speed, JumpPower);
                spriteRenderer.flipX = true;
            }
            else
            {
                SlimeRB.velocity = new Vector2(Speed * -1, JumpPower);
                spriteRenderer.flipX = false;
            }
        }
    }
    private IEnumerator Waiting()
    {
        IsWaiting = true;
        WaitForSeconds wait =  new WaitForSeconds(5f);
        yield return wait;
        IsWaiting = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeComponent();
        InitializeAttribute(0);
        StartCoroutine(FovCheck());
        
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Jump(); 
       
        
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            IsJump = false;

        }
        if (target.gameObject.CompareTag("Bullet"))
        {
            ShootBullet ShootBulletScript = target.gameObject.GetComponent<ShootBullet>();
            float CurrentAttack = ShootBulletScript.GetAttack();
            Health -= CurrentAttack;
            
            HealthBar HealthBarScript = HealthBarObject.GetComponent<HealthBar>();
            HealthBarScript.SetBar(Health, MaxHealth);

            if (Health < 0)
            {
                Destroy(gameObject);
            }
            
        }

    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {


        }

    }
}
