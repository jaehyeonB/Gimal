using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    Vector2 movement = new Vector2();
    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("Loading", 5.0f);
    }

    void Update()
    {

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
}
