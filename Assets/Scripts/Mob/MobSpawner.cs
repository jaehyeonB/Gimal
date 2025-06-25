using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mobPrefabs;            //몹들 프리팹

    [SerializeField]
    private Transform[] spawnPoints;            //몹들이 스폰할 위치

    [SerializeField] private int minCap = 3;          //최소 몹
    [SerializeField] private int maxCap = 6;          //최대 몹

    private readonly List<GameObject> spawned = new();          //현재 살아있는 몹 (신기한 코드네)

    public bool AllMobsDefeated
    {
        get         // <-- 공부할 필요가 있음 | 좀 어려움;
        {
            spawned.RemoveAll(a => a ==  null || !a.activeSelf);
            return spawned.Count == 0;
        }
    }

    public void mobSpawnStart()
    {
        spawned.Clear();

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPoints = new[]
            { transform };
        }
        //여기서 +1의 이유는 (a 이상 , b 미만 이기 떄문!)
        int toSpawn = Random.Range(minCap, maxCap + 1);
        //toSpawn 이 될때까지 소환하기! 근데 괄호 안붙여도 되나? i <= toSpawn
        for(int i = 0; i < toSpawn; i++)
        {
            Transform point = spawnPoints[Random.Range(0,spawnPoints.Length)];
            GameObject prefab = mobPrefabs[Random.Range(0,mobPrefabs.Length)];

            GameObject mob = Instantiate(prefab, point.position, Quaternion.identity, transform);
            spawned.Add(mob);
            Debug.Log("몹 소환 됨");
        }
    }
}
