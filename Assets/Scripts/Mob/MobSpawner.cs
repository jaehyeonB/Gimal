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
                    //0锅 各 家券
                    Debug.Log("0锅 利 家券");
                    break;
                case 1:
                    //1锅 各 家券
                    Debug.Log("1锅 利 家券");
                    break;
                case 2:
                    Debug.Log("2锅 利 家券");
                    break;
                case 3:
                    Debug.Log("3锅 利 家券");
                    break;
                case 4:
                    Debug.Log("4锅 利 家券");
                    break;
            }

            if (i == maxMobCount)
            {
                Debug.Log("家券 场");
            }
            //措面 咯扁俊 各 家券 内靛
        }
    }
}
