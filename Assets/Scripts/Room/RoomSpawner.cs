using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour 
{
    public int openingDirection;
    /*  1 = 아래쪽 문
        2 = 위쪽 문
        3 = 왼쪽 문
        4 = 오른쪽 문 */

    private RoomTemplates templates;            //
    public bool spawned = false;                //false 일 때 방 생성
    public float waitTime = 4f;

    private IEnumerator Start()
    {
        // ① RoomTemplates.Instance 가 준비될 때까지 한 프레임씩 대기
        while (RoomTemplates.Instance == null)
            yield return null;

        templates = RoomTemplates.Instance;

        // ② 정상 동작
        Invoke(nameof(Spawn), 0.1f);
        Invoke(nameof(SpawnClosedRoom), waitTime);
    }

    void Spawn()
    {
        if (spawned) return;

        GameObject[] roomArray = null;

        switch (openingDirection)
        {
            case 1:
                roomArray = templates.topRooms;             //위쪽 문 생성
                break;
            case 2:
                roomArray = templates.bottomRooms;          //아래쪽 문 생성
                break;
            case 3:
                roomArray = templates.rightRooms;           //오른쪽 문 생성
                break;
            case 4:
                roomArray = templates.leftRooms;            //왼쪽 문 생성
                break;
        }

        if (roomArray != null && roomArray.Length > 0)
        {
            int rand = Random.Range(0, roomArray.Length);
            Instantiate(roomArray[rand], transform.position, Quaternion.identity);
            spawned = true;
        }
    }

    void SpawnClosedRoom()
    {
        if (!spawned)
        {
            Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            RoomSpawner otherSpawner = other.GetComponent<RoomSpawner>();
            if (otherSpawner != null && !otherSpawner.spawned && !spawned)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
