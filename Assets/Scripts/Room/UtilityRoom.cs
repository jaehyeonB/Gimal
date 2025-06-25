
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityRoom : MonoBehaviour
{
    public bool traveled = false;
    public int ran;

    public int roomNumber = 0;

    public GameObject[] itemObject;



    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<CameraMove>().RoomMoved(transform);

        if (collision.gameObject.CompareTag("Player") && traveled == false)
        {
            traveled = true;

            ran = Random.Range(1, 4);

            if (ran == 1)
            {
                Debug.Log("아이템 룸");
                roomNumber = 1;         //방 상태 지정
                ItemDrop();

            }

            else if (ran == 2)
            {
                Debug.Log("상점");
            }

            else if (ran == 3)
            {
                Debug.Log("빈방");
            }
        }
    }

    void ItemDrop()
    {
        if (roomNumber == 1)
        {
            
        }
    }
}
