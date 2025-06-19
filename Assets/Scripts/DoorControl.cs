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

    void Start()
    {
        doorT.SetActive(false);
        doorB.SetActive(false);
        doorL.SetActive(false);
        doorR.SetActive(false);
    }

    void Update()
    {

        if(traveled == true && inCombat == false)
        {
            
            Invoke("RoomCleared", 2f);  //Ȯ�ο� Invoke , ���߿� �׳� ���� �� �ڵ� �ֱ�

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<CameraMove>().RoomMoved(transform);

        if (collision.gameObject.CompareTag("Player") && traveled == false)
        {
            
            Invoke("Dungeon", 0.1f);
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
        doorT.SetActive (true);
        doorB.SetActive (true);
        doorL.SetActive (true);
        doorR.SetActive (true);
        inCombat = true;
        

        

        SummonMonsters();
    }

    void SummonMonsters()
    {
        
        traveled = true;
        inCombat = false;
    }

    void RoomCleared()
    {
        doorT.SetActive(false);
        doorB.SetActive(false);
        doorL.SetActive(false);
        doorR.SetActive(false);
        
    }
}
