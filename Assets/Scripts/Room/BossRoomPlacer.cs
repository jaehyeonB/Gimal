using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossRoomPlacer : MonoBehaviour
{
    public GameObject pointBossRoom;
    private IEnumerator Start()
    {

        Debug.Log("BossRoomPlacer ����");

        // �� ���� ���� ������ ��ٸ���
        while (GameObject.FindGameObjectsWithTag("SpawnPoint").Length > 0)
            yield return null;

        Debug.Log("SpawnPoint �����. ������ ��ġ ����");

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

        //deadEnds ã��
        List<Vector2Int> deadEnds = temp.exitCount.Where(kv => kv.Value == 1 && temp.occupiedPositions.Contains(kv.Key)).Select(kv => kv.Key).ToList();
        Debug.Log($"���� ��ϵ� �� ��: {temp.occupiedPositions.Count}");
        foreach (Vector2Int pos in temp.occupiedPositions)
        {
            int exits = 0;
            if (temp.occupiedPositions.Contains(pos + up)) exits++;
            if (temp.occupiedPositions.Contains(pos + down)) exits++;
            if (temp.occupiedPositions.Contains(pos + left)) exits++;
            if (temp.occupiedPositions.Contains(pos + right)) exits++;

            Debug.Log($"�� ��ġ {pos}, ����� �ⱸ ��: {exits}");

            if (exits == 1)
            {
                Debug.Log($"Dead-end ����: {pos}");
                deadEnds.Add(pos);
            }
        }

        if (deadEnds.Count == 0)
        {
            Debug.LogWarning("BossRoomPlacer: Dead-end room not found!");
            return;
        }

        //�������� �ϳ� ����
        Vector2Int bossPos = deadEnds[Random.Range(0, deadEnds.Count)];

        //���� �� ����
        foreach (var r in GameObject.FindGameObjectsWithTag("Rooms"))
            if (Vector2Int.RoundToInt(r.transform.position) == bossPos)
            {
                Vector3 spawnPos = r.transform.position;
                Instantiate(pointBossRoom, spawnPos, Quaternion.identity);
                break;
            }

        //������ ��ġ
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
        int stepX = 0;   // gcd ����
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

        // Ȥ�� ������ ���̸� �� ���� 0�� �� ������ ����
        if (stepX == 0) stepX = stepY == 0 ? 1 : stepY;
        if (stepY == 0) stepY = stepX;

        Debug.Log($"[BossRoomPlacer] ������ step = ({stepX},{stepY})");
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