using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockedRoom : MonoBehaviour
{
    public TextMeshProUGUI tmp;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("이 방은 막혀 있습니다...");
        }
    }
}
