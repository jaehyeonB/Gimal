using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public Animator ani;


    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        
       
    }

    public void openDoor()
    {
        Debug.Log("¿€µø!");
        ani.SetBool("IsOpen", true);
    }

    public void closeDoor()
    {
        ani.SetBool("IsOpen", false);
    }
}
