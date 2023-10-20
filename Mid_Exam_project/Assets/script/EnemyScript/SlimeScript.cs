using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class SlimeScript : MonoBehaviour
{

    // slime attribute
    private int Health;
    private int Defense;
    private float Attack;
    private int AttackSpeed;
    private int MaxHealth;
    private int MaxDefense;
    private float Speed;

    // Raycast
    private float FieldOfView;
    public LayerMask TargetLayer;
    public LayerMask ObstructLayer;
    public GameObject PlayerRef;
    public bool cansee { get; private set; }
    private bool Direction;


    // Movement
    private float JumpPower;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D SlimeRB;
    private bool IsJump;
    private void InitializeComponent()
    {
        SlimeRB = GetComponent<Rigidbody2D>();
        IsJump = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerRef = GameObject.Find("Player");
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

        JumpPower = 5f;
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

    private void FOV()
    {
        Collider2D[] RangeCheck = Physics2D.OverlapCircleAll(transform.position, FieldOfView, TargetLayer);
        if (RangeCheck.Length > 0)
        {
            Transform Target = RangeCheck[0].transform;
            Vector2 TargetDirection = (Target.position - transform.position).normalized;
            float TargetDistance = Vector2.Distance(transform.position, Target.position);
            Debug.Log(TargetDirection);
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
                Debug.Log("I Can See");
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
        if (!IsJump)
        {
            int Jump = Random.Range(0, 100);
            if(Jump< 30)
            {
                Move();
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            //if (target.contacts[0].normal.y == 1)
            //{
                
            //}
            IsJump = false;
            Debug.Log(IsJump);

        }

    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            IsJump = true;
            Debug.Log(IsJump);
        }

    }
}
