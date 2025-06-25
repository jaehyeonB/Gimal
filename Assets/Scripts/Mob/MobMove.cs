using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MonoBehaviour
{
    [Header("몹 이동")]
    public float speed = 2.5f;
    private Transform player;

    [Header("몹 체력")]
    public int maxHealth = 3;
    public int currentHealth = 3;

    Rigidbody2D rb;
    //Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        // 태그로 플레이어 찾기
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
        Debug.Log("피격!");
        currentHealth -= damage;
        //if (animator != null) animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        //애니메이션 넣으려면 윗줄에 애니 코드 넣고 Destroy 에 시간 지연 시키기
        Destroy(gameObject);
    }
}
