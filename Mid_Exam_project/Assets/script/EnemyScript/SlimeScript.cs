using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    // Line of Sight
    private float FieldOfView;
    [RangeAttribute(0, 360)] private float ViewAngle;
    public LayerMask TargetLayer;
    public LayerMask ObstructLayer;
    public GameObject PlayerRef;
    public Transform EyePos;
    public bool cansee { get; private set; }

    // Movement
    private float JumpPower;


    private Rigidbody2D SlimeRB;
    private bool IsJump;
    private void InitializeComponent()
    {
        SlimeRB = GetComponent<Rigidbody2D>();
        IsJump = true;
    }
    private void InitializeAttribute(int level)
    {


        MaxHealth = 20 + (10 * level);
        MaxDefense = 3 + (1 * level);
        Attack = 2 + (float)(0.5 * level);
        Health = MaxHealth;
        Defense = MaxDefense;
        Speed = 0.5f;


        JumpPower = 5f;

        FieldOfView = 5f;
        ViewAngle = 45;
    }
    private void Move()
    {
        SlimeRB.velocity = new Vector2(SlimeRB.velocity.x, JumpPower);
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
        Collider2D[] RangeCheck = Physics2D.OverlapCircleAll(EyePos.position, FieldOfView, TargetLayer);
        if (RangeCheck.Length > 0)
        {
            Transform Target = RangeCheck[0].transform;
            Vector2 TargetDirection = (Target.position - EyePos.position).normalized;

            if (Vector2.Angle(Vector2.up, TargetDirection) < ViewAngle / 2)
            {
                float TargetDistance = Vector2.Distance(EyePos.position, Target.position);
                Debug.Log("I Can See");
                if (!Physics2D.Raycast(EyePos.position, TargetDirection, TargetDistance, ObstructLayer))
                {
                    cansee = true;
                    Debug.Log("I Can See");
                }
                else
                {
                    cansee = false;
                }
            }
            else
            {
                cansee = false;
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
        UnityEditor.Handles.DrawWireDisc(EyePos.position, Vector3.forward, FieldOfView);
        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -ViewAngle/2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z,ViewAngle/2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(EyePos.position, EyePos.position + angle01 * FieldOfView);
        Gizmos.DrawLine(EyePos.position, EyePos.position + angle02 * FieldOfView);

        if (cansee)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(EyePos.position, PlayerRef.transform.position);
        }
    }
    private Vector2 DirectionFromAngle(float eulerY,float AngleDegrees)
    {
        AngleDegrees += eulerY;

        return new Vector2(Mathf.Sin(AngleDegrees * Mathf.Deg2Rad), Mathf.Cos(AngleDegrees * Mathf.Deg2Rad));
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
            if (Jump < 30)
            {
                Move();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            IsJump = false;

            

        }

    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Tiles"))
        {
            IsJump = true;
            
        }

    }
}
