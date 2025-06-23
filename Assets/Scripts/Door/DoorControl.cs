using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public bool traveled = false;
    public bool inCombat = false;
    public int activeMob = 3;
    public int maxMob = 10;
    

    public GameObject doorT;
    public GameObject doorB;
    public GameObject doorL;
    public GameObject doorR;

    GameObject mobSpawn;

    GameObject doorBAni;

    void Start()
    {
        mobSpawn = GameObject.Find("mobSpawnPoint");

        doorBAni = GameObject.Find("doorB");

        doorT.GetComponent<BoxCollider2D>().enabled = false;        //대충 문 콜라이더 꺼주는 코드 라는 뜻.
        doorB.GetComponent<BoxCollider2D>().enabled = false;
        doorL.GetComponent<BoxCollider2D>().enabled = false;
        doorR.GetComponent<BoxCollider2D>().enabled = false;

    }

    void Update()
    {

        if(traveled == true && inCombat == false)
        {
            
            Invoke("RoomCleared", 2f);  

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Player") && traveled == false)         //Compare Tag "Player" 와 충돌하였을 때 && traveled 값이 false 이면
        {
         
            Invoke("Dungeon", 0.1f);            //던전 함수를 0.1초 후 실행 || 0.1초 후에 실행하는건 문을 닫을 경우 너무 빨리 닫으면 플레이어가 못들어감
        }
        /*
        if(collision.gameObject.CompareTag("Mob"))
        {
            activeMob++;
        }
        */


    }
    void Dungeon()
    {
        doorT.GetComponent<BoxCollider2D>().enabled = true;
        //아래쪽 문 닫기
        doorB.GetComponent<BoxCollider2D>().enabled = true;
        DoorAnimation.Instance.closeDoor();
        doorL.GetComponent<BoxCollider2D>().enabled = true;
        doorR.GetComponent<BoxCollider2D>().enabled = true;

        inCombat = true;                    //에휴; 전투중 활성화

        

        SummonMonsters();
    }

    void SummonMonsters()
    {

        mobSpawn.GetComponent<MobSpawner>().mobSpawnStart();
        traveled = true;
        inCombat = false;
        
    }

    void RoomCleared()
    {
        
        doorT.GetComponent<BoxCollider2D>().enabled = false;
        doorB.GetComponent<BoxCollider2D>().enabled = false;
        doorL.GetComponent<BoxCollider2D>().enabled = false;
        doorR.GetComponent<BoxCollider2D>().enabled = false;
        
    }
}
