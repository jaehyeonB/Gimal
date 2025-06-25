using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mobPrefabs;            //���� ������

    [SerializeField]
    private Transform[] spawnPoints;            //������ ������ ��ġ

    [SerializeField] private int minCap = 3;          //�ּ� ��
    [SerializeField] private int maxCap = 6;          //�ִ� ��

    private readonly List<GameObject> spawned = new();          //���� ����ִ� �� (�ű��� �ڵ��)

    public bool AllMobsDefeated
    {
        get         // <-- ������ �ʿ䰡 ���� | �� �����;
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
        //���⼭ +1�� ������ (a �̻� , b �̸� �̱� ����!)
        int toSpawn = Random.Range(minCap, maxCap + 1);
        //toSpawn �� �ɶ����� ��ȯ�ϱ�! �ٵ� ��ȣ �Ⱥٿ��� �ǳ�? i <= toSpawn
        for(int i = 0; i < toSpawn; i++)
        {
            Transform point = spawnPoints[Random.Range(0,spawnPoints.Length)];
            GameObject prefab = mobPrefabs[Random.Range(0,mobPrefabs.Length)];

            GameObject mob = Instantiate(prefab, point.position, Quaternion.identity, transform);
            spawned.Add(mob);
            Debug.Log("�� ��ȯ ��");
        }
    }
}
