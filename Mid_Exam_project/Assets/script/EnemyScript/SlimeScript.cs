using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    }
    private void InitializeAttribute(int level)
    {


        MaxHealth = 20 + (10 * level);
        MaxDefense = 3 + (1 * level);
        Attack = 2 + (float)(0.5 * level);
        Health = MaxHealth;
        Defense = MaxDefense;
        Speed = 1f;


        JumpPower = 5f;
    }
    private void Move()
    {
        int LeftOrRight = Random.Range(-1, 2);
        if(LeftOrRight  < 0)
        {
            SlimeRB.velocity = new Vector2(Speed, JumpPower);
            spriteRenderer.flipX = true;
        }
        else if(LeftOrRight > 0)
        {
            SlimeRB.velocity = new Vector2(Speed*-1, JumpPower);
            spriteRenderer.flipX = false;
        }

        
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeComponent();
        InitializeAttribute(0);
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
