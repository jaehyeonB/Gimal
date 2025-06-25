using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("�÷��̾� �̵�")]
    public float moveSpeed = 10f;

    [Header("�÷��̾� ����")]
    public float attackRange = 1f;
    public int attackDamage = 1;
    public Transform attackPoint;
    private LayerMask enemyLayers;
    public float attackRate = 2f;

    private float nextAttackTime = 0f;

    [Header("�÷��̾� ü��")]
    public int maxHealth = 5;
    public int currentHealth = 0;

    //attackPoint ��ġ ������
    public Vector2 offsetTop = new(0f, 0.7f);
    public Vector2 offsetBottom = new(0f, -0.7f);
    public Vector2 offsetLeft = new(-0.7f, 0f);
    public Vector2 offsetRight = new(0.7f, 0f);
    private Vector2 lastLookDir = Vector2.right;

    Vector2 movement = new Vector2();           //���� �̰� �� ���������?
    Rigidbody2D rb;
    Animator animator;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();

        enemyLayers = LayerMask.GetMask("Enemy");
        currentHealth = maxHealth;
        Invoke("Loading", 5.0f);
    }

    void Update()
    {
        //�̵� �ڵ�
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if(movement.sqrMagnitude > 0.001f)
        {
            if(Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                lastLookDir = new Vector2(Mathf.Sign(movement.x), 0);
            }
            else
            {
                lastLookDir = new Vector2(0, Mathf.Sign(movement.y));
            }
        }

        //���� �ڵ�
        if (Time.time >= nextAttackTime && Input.GetMouseButtonDown(0))
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;         //�� �� ������ �ϴ°ɱ�?
    }

    private void LateUpdate()
    {
        if (attackPoint == null)
            return;

        if(lastLookDir.x > 0)
        {
            attackPoint.localPosition = offsetRight;
        }
        else if(lastLookDir.x < 0)
        {
            attackPoint.localPosition = offsetLeft;
        }
        else if(lastLookDir.y > 0)
        {
            attackPoint.localPosition = offsetTop;
        }
        else if(lastLookDir.y < 0)
        {
            attackPoint.localPosition = offsetBottom;
        }
    }


    void Loading()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.gameObject.CompareTag("RoomCheck"))
        {
            
        }
        */
    }

    void Attack()
    {
        
        // �ִϸ��̼� ���
        //�ӽ� �ּ� animator.SetTrigger("Attack");
        Debug.Log($"mask:{enemyLayers.value}  range:{attackRange}  pos:{attackPoint.position}");
        // �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        Debug.Log($"  OverlapCircleAll ���: {hitEnemies.Length}��");

        foreach (Collider2D enemy in hitEnemies)
        {
            // ������ ������ �ֱ�
            Debug.Log("����!");
            enemy.GetComponent<MobMove>()?.TakeDamage(attackDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
