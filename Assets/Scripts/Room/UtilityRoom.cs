using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityRoom : MonoBehaviour
{
    public bool traveled = false;
    public int ran;

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
                Debug.Log("코인룸");
            }

            if (ran == 2)
            {
                Debug.Log("상점");
            }

            if (ran == 3)
            {
                Debug.Log("빈방");
            }
        }
    }

    void coinDrop()
    {
        //대충 돈 스폰시키는 코드
        //Random.Range(1, 3);
    }
}
