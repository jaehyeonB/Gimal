using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public bool traveled = false;
    

    public GameObject doorT;
    public GameObject doorB;
    public GameObject doorL;
    public GameObject doorR;

    void Start()
    {
        doorT.SetActive(false);
        doorB.SetActive(false);
        doorL.SetActive(false);
        doorR.SetActive(false);
    }

    void Update()
    {

        if(traveled == true)
        {
            Debug.Log("클리어");
            Invoke("RoomCleared", 2f);  //확인용 Invoke , 나중에 그냥 여따 문 코드 넣기
            Destroy(gameObject, 5f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && traveled == false)
        {
            Debug.Log("충돌");
            Invoke("Dungeon", 0.1f);
        }
    }
    void Dungeon()
    {
        doorT.SetActive (true);
        doorB.SetActive (true);
        doorL.SetActive (true);
        doorR.SetActive (true);
        Debug.Log("문 소환!");

        SummonMonsters();
    }

    void SummonMonsters()
    {
        Debug.Log("몬스터 스폰!");
        //대충 몬스터 소환하는 스크립트
        traveled = true;
    }

    void RoomCleared()
    {
        doorT.SetActive(false);
        doorB.SetActive(false);
        doorL.SetActive(false);
        doorR.SetActive(false);
    }
}
