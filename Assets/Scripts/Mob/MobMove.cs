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
        if(player == null) 
            return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("�ǰ�!");
        currentHealth -= damage;
        //if (animator != null) animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        //�ִϸ��̼� �������� ���ٿ� �ִ� �ڵ� �ְ� Destroy �� �ð� ���� ��Ű��
        Destroy(gameObject);
    }
}
