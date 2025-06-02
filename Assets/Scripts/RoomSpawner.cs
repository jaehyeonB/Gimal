using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 --> �Ʒ��� �� �ʿ�
    //2 --> ���� �� �ʿ�
    //3 --> ���� �� �ʿ�
    //4 --> ������ �� �ʿ�

    private RoomTemplate template;
    private int rand;
    private bool spawned = false;

    private void Start()
    {
        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("spawn", 0.1f);
    }

    void spawn()
    {
        Debug.Log("!");
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //�ٴ��� �� ����
                rand = Random.Range(0, template.bottomRooms.Length);
                Instantiate(template.bottomRooms[rand], transform.position, template.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                //���� �� ����
                rand = Random.Range(0, template.topRooms.Length);
                Instantiate(template.topRooms[rand], transform.position, template.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                //���� �� ����
                rand = Random.Range(0, template.leftRooms.Length);
                Instantiate(template.leftRooms[rand], transform.position, template.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //������ �� ����
                rand = Random.Range(0, template.rightRooms.Length);
                Instantiate(template.rightRooms[rand], transform.position, template.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
