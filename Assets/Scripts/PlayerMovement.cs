
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [Header("플레이어 이동")]
    public float moveSpeed = 10f;

    [Header("플레이어 공격")]
    public float attackRange = 1f;
    public int attackDamage = 1;
    public Transform attackPoint;
    private LayerMask enemyLayers;
    public float attackRate = 2f;

    private float nextAttackTime = 0f;

    [Header("플레이어 체력")]
    public int maxHealth = 5;
    public int currentHealth = 0;
    public bool isInvincible = false;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    //attackPoint 위치 조정용
    public Vector2 offsetTop = new(0f, 0.7f);
    public Vector2 offsetBottom = new(0f, -0.7f);
    public Vector2 offsetLeft = new(-0.7f, 0f);
    public Vector2 offsetRight = new(0.7f, 0f);
    private Vector2 lastLookDir = Vector2.right;

    Vector2 movement = new Vector2();           //내가 이걸 왜 적어놨더라?
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();

        enemyLayers = LayerMask.GetMask("Enemy");
        currentHealth = maxHealth;
        Invoke("Loading", 5.0f);
    }

    void Update()
    {
        //이동 코드
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetBool("Run",true);
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetBool("Run",true);

        movement.Normalize();

        if(movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("Run", false);
        }

        if(movement.x > 0)
        {
            spriteRenderer.flipX= true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

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

        //공격 코드
        if (Time.time >= nextAttackTime && Input.GetMouseButtonDown(0))
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if(currentHealth == 4)
        {
            heart5.gameObject.SetActive(false);
        }
        else if(currentHealth == 3)
        {
            heart4.gameObject.SetActive(false);
        }
        else if(currentHealth == 2)
        {
            heart3.gameObject.SetActive(false);
        }
        else if(currentHealth == 1)
        {
            heart2.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;         //얜 왜 적으라 하는걸까?
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

    void Attack()
    {
        
        // 애니메이션 재생
        //임시 주석 animator.SetTrigger("Attack");
        Debug.Log($"mask:{enemyLayers.value}  range:{attackRange}  pos:{attackPoint.position}");
        // 적 감지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        Debug.Log($"  OverlapCircleAll 결과: {hitEnemies.Length}개");

        foreach (Collider2D enemy in hitEnemies)
        {
            // 적에게 데미지 주기
            Debug.Log("공격!");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Mob") && isInvincible == false)
        {
            isInvincible = true;
            moveSpeed += 5;
            currentHealth--;

            if(currentHealth <= 0)
            {
                //Json
                SceneManager.LoadScene("Title");
            }

            Invoke("InvinceOff", 3f);
        }
    }

    void InvinceOff()
    {
        isInvincible = false;
        for (int i = 0; i < 10; i++)
        {
            moveSpeed -= 0.5f;
        }

    }
}
