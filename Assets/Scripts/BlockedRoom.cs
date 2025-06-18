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
        Debug.Log("이 방은 막혀있다..");
    }
}
