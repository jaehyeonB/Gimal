using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MonoBehaviour
{
    [Header("�� �̵�")]
    public float speed = 2.5f;
    private Transform player;

    [Header("�� ü��")]
    public int maxHealth = 3;
    public int currentHealth = 3;

    [Header("�˹�")]
    public float knockbackForce = 20f;      // �ѹ��� �и��� ��
    public float knockbackTime = 0.5f;   // �и� �� �����ִ� �ð�
    public bool isKnockback;

    Rigidbody2D rb;
    //Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        // �±׷� �÷��̾� ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if(player == null || isKnockback) 
            return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("�ǰ�!");
        currentHealth -= damage;
        //if (animator != null) animator.SetTrigger("Hit");

        Vector2 attackerPos = new();

        Vector2 knockDir = (transform.position - (Vector3)attackerPos).normalized;
        StartCoroutine(Knockback(knockDir));

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator Knockback(Vector2 dir)
    {
        isKnockback = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);

        rb.velocity = Vector2.zero;   // ���� (���� ����)
        isKnockback = false;
    }

    private void Die()
    {

        //�ִϸ��̼� �������� ���ٿ� �ִ� �ڵ� �ְ� Destroy �� �ð� ���� ��Ű��
        Destroy(gameObject);
    }
}
