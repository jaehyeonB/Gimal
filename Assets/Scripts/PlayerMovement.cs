using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("�÷��̾� �̵�")]
    public float moveSpeed = 10.0f;

    [Header("�÷��̾� ����")]
    public float attackRange = 1f;
    public int attackDamage = 1;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRate = 2f;

    [Header("�÷��̾� ü��")]
    public int maxHealth = 5;
    public int currentHealth = 0;

    private float nextAttackTime = 0f;
    private Animator animator;

    Vector2 movement = new Vector2();           //���� �̰� �� ���������?
    Rigidbody2D rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();

        currentHealth = maxHealth;
        Invoke("Loading", 5.0f);
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0)) // ���� ���콺 Ŭ��
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb.velocity = movement * moveSpeed;
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

        // �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // ������ ������ �ֱ�
            Debug.Log("����!");
            enemy.GetComponent<MobMove>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
