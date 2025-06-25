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
        while (RoomTemplates.Instance == null)
            yield return null;

        templates = RoomTemplates.Instance;
        Vector2Int pos = Vector2Int.RoundToInt(transform.position);
        RoomTemplates.Instance.occupiedPositions.Add(pos);
        Debug.Log($"RoomSpawner 위치 기록됨: {pos}");

        Invoke(nameof(Spawn), 0.1f);
        Invoke(nameof(SpawnClosedRoom), waitTime);
    }

    private void Awake()
    {
        Destroy(gameObject, 4.25f);
    }

    void Spawn()
    {
        if (spawned) return;

        Vector2Int pos = Vector2Int.RoundToInt(transform.position);
        templates.occupiedPositions.Add(pos);

        templates.RegisterExit(pos, openingDirection);   //출구 기록
        spawned = true;

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

            RoomTemplates.Instance.occupiedPositions.Add(Vector2Int.RoundToInt(transform.position));
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
