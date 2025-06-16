using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public bool traveled = false;
    

    public GameObject doorT;
    public GameObject doorB;
    public GameObject doorL;
    public GameObject doorR;

    void Start()
    {
        doorT.SetActive(false);
        doorB.SetActive(false);
        doorL.SetActive(false);
        doorR.SetActive(false);
    }

    void Update()
    {

        if(traveled == true)
        {
            Debug.Log("Ŭ����");
            Invoke("RoomCleared", 2f);  //Ȯ�ο� Invoke , ���߿� �׳� ���� �� �ڵ� �ֱ�
            Destroy(gameObject, 5f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && traveled == false)
        {
            Debug.Log("�浹");
            Invoke("Dungeon", 0.1f);
        }
    }
    void Dungeon()
    {
        doorT.SetActive (true);
        doorB.SetActive (true);
        doorL.SetActive (true);
        doorR.SetActive (true);
        Debug.Log("�� ��ȯ!");

        SummonMonsters();
    }

    void SummonMonsters()
    {
        Debug.Log("���� ����!");
        //���� ���� ��ȯ�ϴ� ��ũ��Ʈ
        traveled = true;
    }

    void RoomCleared()
    {
        doorT.SetActive(false);
        doorB.SetActive(false);
        doorL.SetActive(false);
        doorR.SetActive(false);
    }
}
