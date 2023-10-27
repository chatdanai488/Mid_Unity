using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardBossScript : MonoBehaviour
{
    // slime attribute
    private float Health;
    private float Defense;
    private float Attack;
    private float AttackSpeed;
    private int MaxHealth;
    private int MaxDefense;
    private float Speed;

    private int RandomLevel;

    // Raycast
    private float FieldOfView;
    public LayerMask TargetLayer;
    public LayerMask ObstructLayer;
    public LayerMask EnemyLayer;
    private GameObject PlayerRef;
    public bool cansee { get; private set; }
    private bool Direction;
    private bool IsWaiting;

    private BoxCollider2D boxCollider2d;

    // Movement
    private float JumpPower;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D LizardRB;
    private bool IsJump;

    // HealthBar
    public GameObject HealthBarObject;
    private HealthBar HealthBarScript;

    // Attack
    public GameObject LizardBulletPrefab;
    public GameObject LizardGunPoint;
    private bool PreviousValue;
    private bool IsShoot;


    public GameObject CoinPrefab;
    public int level;

    private Animator anim;
    private void InitializeComponent()
    {
        LizardRB = GetComponent<Rigidbody2D>();
        IsJump = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerRef = GameObject.Find("Player");
        boxCollider2d = GetComponent<BoxCollider2D>();
        IsWaiting = false;

        PreviousValue = false;
        IsShoot = false;
    }
    private void InitializeAttribute(int level)
    {


        MaxHealth = 500 + (40 * level);
        MaxDefense = 3 + (1 * level);
        Attack = 10 + (float)(0.5 * level);
        Health = MaxHealth;
        Defense = MaxDefense;
        Speed = 1f;

        FieldOfView = 30f;

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

    private void FOV()
    {
        Collider2D[] RangeCheck = Physics2D.OverlapCircleAll(transform.position, FieldOfView, TargetLayer);
        if (RangeCheck.Length > 0)
        {
            Transform Target = RangeCheck[0].transform;
            Vector2 TargetDirection = (Target.position - transform.position).normalized;
            float TargetDistance = Vector2.Distance(transform.position, Target.position);
            Debug.Log("Checking For Player");
            if (TargetDirection.x > 0)
            {
                Direction = true;
                spriteRenderer.flipX = true;
            }
            else
            {
                Direction = false;
                spriteRenderer.flipX = false;
            }

            if (!Physics2D.Raycast(transform.position, TargetDirection, TargetDistance, ObstructLayer))
            {
                cansee = true;
                //FacePlayer(Direction);
                Debug.Log("Player Spotted");
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
        else
        {
            Gizmos.color = Color.red;
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


        if (Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, ObstructLayer) || Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, EnemyLayer))
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
        if (!IsJump && !IsWaiting && !cansee)
        {
            int Jump = Random.Range(0, 100);
            if (Jump < 100)
            {
                Move();
            }
            StartCoroutine(Waiting());
            Debug.Log("Jump");
        }

    }
    private void Move()
    {


        int LeftOrRight = Random.Range(-1, 2);
        if (LeftOrRight < 0)
        {
            LizardRB.velocity = new Vector2(Speed, JumpPower);
            spriteRenderer.flipX = true;
        }
        else if (LeftOrRight > 0)
        {
            LizardRB.velocity = new Vector2(Speed * -1, JumpPower);
            spriteRenderer.flipX = false;
        }

        Debug.Log("Move");
    }
    private IEnumerator Waiting()
    {
        IsWaiting = true;
        yield return new WaitForSeconds(3);
        IsWaiting = false;
    }
    public float GetAttack()
    {
        float AttackMultiplier = Random.Range(-5, 6);
        float CurrentAttack = Attack + Attack * AttackMultiplier / 20;

        return CurrentAttack;
    }
    public bool GetDirection()
    {
        return spriteRenderer.flipX;
    }
    private IEnumerator AttackCooldown()
    {
        IsShoot = true;
        yield return new WaitForSeconds(3);
        IsShoot = false;
    }
    private void Shoot()
    {
        if (!IsShoot)
        {

            ShootBullet(spriteRenderer.flipX);
            StartCoroutine(AttackCooldown());

        }
    }
    private void ShootBullet(bool value)
    {

        GameObject Bullet = Instantiate(LizardBulletPrefab, LizardGunPoint.transform.position, Quaternion.identity);
        Bullet.transform.localScale = Vector3.one * 30;
        LizardShootBullet LizardShootBulletScript = Bullet.GetComponent<LizardShootBullet>();
        LizardShootBulletScript.GetValue(value);
        LizardShootBulletScript.SetAttribute(Attack);

    }
    private void FlipShootPoint(bool value)
    {
        if (value == true)
        {

            LizardGunPoint.transform.position = new Vector3(LizardGunPoint.transform.position.x + 20f, LizardGunPoint.transform.position.y, LizardGunPoint.transform.position.z);
        }
        else if (value == false)
        {

            LizardGunPoint.transform.position = new Vector3(LizardGunPoint.transform.position.x - 20f, LizardGunPoint.transform.position.y, LizardGunPoint.transform.position.z);
        }
    }

    private void FacePlayer(bool value)
    {
        if (value)
        {
            spriteRenderer.flipX = true;
            FlipShootPoint(spriteRenderer.flipX);
        }
        else
        {
            spriteRenderer.flipY = false;
            FlipShootPoint(spriteRenderer.flipX);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeComponent();
        RandomLevel = Random.Range(0, 5);
        InitializeAttribute(RandomLevel);
        StartCoroutine(FovCheck());

    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Jump();
        if (cansee)
        {
            Shoot();
        }
        if (PreviousValue != spriteRenderer.flipX)
        {
            FlipShootPoint(spriteRenderer.flipX);
            PreviousValue = spriteRenderer.flipX;
        }
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
                int CoinCount = Random.Range(100, 200) * RandomLevel;
                for(int i = 0; i<CoinCount; i++)
                {
                    GameObject Coin = Instantiate(CoinPrefab, transform);
                }

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
