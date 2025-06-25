using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossRoomPlacer : MonoBehaviour
{
    public GameObject pointBossRoom;
    private IEnumerator Start()
    {

        Debug.Log("BossRoomPlacer 시작");

        // 방 생성 끝날 때까지 기다리기
        while (GameObject.FindGameObjectsWithTag("SpawnPoint").Length > 0)
            yield return null;

        Debug.Log("SpawnPoint 사라짐. 보스방 배치 시작");

        PlaceBossRoom();
    }

    void PlaceBossRoom()
    {
        var temp = RoomTemplates.Instance;
        if (temp == null || temp.bossPlaced) return;

        Vector2Int step = DetectStep(temp.occupiedPositions);
        var up = new Vector2Int(0, step.y);
        var down = new Vector2Int(0, -step.y);
        var left = new Vector2Int(-step.x, 0);
        var right = new Vector2Int(step.x, 0);

        //deadEnds 찾기
        List<Vector2Int> deadEnds = temp.exitCount.Where(kv => kv.Value == 1 && temp.occupiedPositions.Contains(kv.Key)).Select(kv => kv.Key).ToList();
        Debug.Log($"현재 등록된 방 수: {temp.occupiedPositions.Count}");
        foreach (Vector2Int pos in temp.occupiedPositions)
        {
            int exits = 0;
            if (temp.occupiedPositions.Contains(pos + up)) exits++;
            if (temp.occupiedPositions.Contains(pos + down)) exits++;
            if (temp.occupiedPositions.Contains(pos + left)) exits++;
            if (temp.occupiedPositions.Contains(pos + right)) exits++;

            Debug.Log($"방 위치 {pos}, 연결된 출구 수: {exits}");

            if (exits == 1)
            {
                Debug.Log($"Dead-end 감지: {pos}");
                deadEnds.Add(pos);
            }
        }

        if (deadEnds.Count == 0)
        {
            Debug.LogWarning("BossRoomPlacer: Dead-end room not found!");
            return;
        }

        //랜덤으로 하나 선택
        Vector2Int bossPos = deadEnds[Random.Range(0, deadEnds.Count)];

        //기존 방 제거
        foreach (var r in GameObject.FindGameObjectsWithTag("Rooms"))
            if (Vector2Int.RoundToInt(r.transform.position) == bossPos)
            {
                Vector3 spawnPos = r.transform.position;
                Instantiate(pointBossRoom, spawnPos, Quaternion.identity);
                break;
            }

        //보스방 배치
        /*Instantiate(temp.bossRoomPrefab, (Vector2)bossPos, Quaternion.identity);
        temp.bossPlaced = true;*/
        Debug.Log($"Boss room placed at {bossPos}");
        GameObject boss = Instantiate(temp.bossRoomPrefab,
                              (Vector2)bossPos, Quaternion.identity);
        boss.name = "BossRoom";
        boss.tag = "BossRoom";

        temp.bossPlaced = true;
        Debug.Log($"Boss room placed at {bossPos}");
    }

    Vector2Int DetectStep(HashSet<Vector2Int> posSet)
    {
        int stepX = 0;   // gcd 누적
        int stepY = 0;

        foreach (var a in posSet)
        {
            foreach (var b in posSet)
            {
                if (a == b) continue;
                int dx = Mathf.Abs(a.x - b.x);
                int dy = Mathf.Abs(a.y - b.y);

                if (dx != 0) stepX = stepX == 0 ? dx : GCD(stepX, dx);
                if (dy != 0) stepY = stepY == 0 ? dy : GCD(stepY, dy);
            }
        }

        // 혹시 일직선 맵이면 한 축이 0일 수 있으니 보정
        if (stepX == 0) stepX = stepY == 0 ? 1 : stepY;
        if (stepY == 0) stepY = stepX;

        Debug.Log($"[BossRoomPlacer] 감지된 step = ({stepX},{stepY})");
        return new Vector2Int(stepX, stepY);
    }

    int GCD(int a, int b)
    {
        while (b != 0)
        {
            int t = a % b;
            a = b;
            b = t;
        }
        return a;
    }
}