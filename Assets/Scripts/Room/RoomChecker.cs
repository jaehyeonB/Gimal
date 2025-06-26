using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    public bool travelled = false;
    public bool inCombat = false;
    public bool isBossRoom = false;

    //문들 지정
    [SerializeField]    private DoorOpener doorT;                   //위쪽 문
    [SerializeField]    private DoorOpener doorB;                   //아래쪽 문
    [SerializeField]    private DoorOpener doorL;                   //왼쪽 문
    [SerializeField]    private DoorOpener doorR;                   //오른쪽 문
    //스포너 지정
    [SerializeField]    private MobSpawner mobSpawner;                //몹 스포너


    private void Update()
    {
        //몹 스포너 스크립트가 없거나 or 몬스터를 다 잡았을 때 방 클리어
        if(inCombat && (mobSpawner == null || mobSpawner.AllMobsDefeated))
        {
            RoomCleared();
        }

    }

    private void Start()
    {
        if (!travelled)
        {
            OpenAllDoors();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BossRoomPointer"))
        {
            isBossRoom = true;
        }

        if (other.gameObject.CompareTag("Player") && travelled == false && isBossRoom == false)         //Compare Tag "Player" 와 충돌하였을 때 && traveled 값이 false 이면
        {
            Invoke("StartDungeon", 0.1f);            //던전 함수를 0.1초 후 실행 || 0.1초 후에 실행하는건 문을 닫을 경우 너무 빨리 닫으면 플레이어가 못들어감
        }

    }

    void StartDungeon()
    {

        inCombat = true;                    //전투중 활성화

        CloseAllDoors();                    //문 닫기 함수

        if(mobSpawner != null && isBossRoom == false)
        {
            mobSpawner.mobSpawnStart();                //몹 소환 함수
            Debug.Log("소환!");
        }

    }

    void RoomCleared()
    {

        inCombat = false;
        travelled = true;

        OpenAllDoors();

    }

    private void CloseAllDoors()
    {
        if (doorT != null) doorT.CloseDoor();
        if (doorB != null) doorB.CloseDoor();
        if (doorL != null) doorL.CloseDoor();
        if (doorR != null) doorR.CloseDoor();
    }

    private void OpenAllDoors()
    {
        if (doorT != null) doorT.OpenDoor();
        if (doorB != null) doorB.OpenDoor();
        if (doorL != null) doorL.OpenDoor();
        if (doorR != null) doorR.OpenDoor();
    }
}
