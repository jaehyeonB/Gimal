using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] mobs;

    public int mobType;

    public void Awake()
    {
        
    }

    public void mobSpawnStart()
    {
        int maxMobCount;

        maxMobCount = Random.Range(3, 8);

        for(int i = 0; i <= maxMobCount; i++)
        {
            mobType = Random.Range(0, 5);

            switch(mobType)
            {
                case 0:
                    //0�� �� ��ȯ
                    Debug.Log("0�� �� ��ȯ");
                    break;
                case 1:
                    //1�� �� ��ȯ
                    Debug.Log("1�� �� ��ȯ");
                    break;
                case 2:
                    Debug.Log("2�� �� ��ȯ");
                    break;
                case 3:
                    Debug.Log("3�� �� ��ȯ");
                    break;
                case 4:
                    Debug.Log("4�� �� ��ȯ");
                    break;
            }

            if (i == maxMobCount)
            {
                Debug.Log("��ȯ ��");
            }
            //���� ���⿡ �� ��ȯ �ڵ�
        }
    }
}
