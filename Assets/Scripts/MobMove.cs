using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MonoBehaviour
{
    public float speed = 5f;
    private Transform player;

    void Start()
    {
        // �±׷� �÷��̾� ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
}
