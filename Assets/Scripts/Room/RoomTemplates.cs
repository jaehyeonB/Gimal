using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;             //막힌 방 프리팹

    // 생성된 방의 위치 추적
    public HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();
    public Dictionary<Vector2Int, int> exitCount = new();

    [Header("Boss")]
    public GameObject bossRoomPrefab;
    public bool bossPlaced = false;

    public static RoomTemplates Instance { get; private set; }

    private void Awake()
    {
        //방생성이 갑작히 안돼서 GPT 가 준 해결책 (연구 필요)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterExit(Vector2Int pos, int openingDir)
    {
        // 자기 방 1증가
        if (!exitCount.ContainsKey(pos)) exitCount[pos] = 0;
        exitCount[pos]++;

        // 맞은편 방도 미리 1증가
        Vector2Int offset = openingDir switch
        {
            1 => new(0, -1),   // 아래쪽 문
            2 => new(0, 1),   // 위쪽 문
            3 => new(-1, 0),   // 왼쪽 문
            4 => new(1, 0),   // 오른쪽 문
            _ => Vector2Int.zero
        };
        Vector2Int neigh = pos + offset;
        if (!exitCount.ContainsKey(neigh)) exitCount[neigh] = 0;
        exitCount[neigh]++;   // 나중에 진짜 방 생기면 값이 1 → 2 로 갱신됨
    }
}
