using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public static DoorAnimation Instance;
    public Animator ani;


    void Start()
    {
        Instance = this;
        ani.SetBool("isopen", true);
    }

    void Update()
    {
        
    }

    public void openDoor()
    {
        ani.SetBool("isopen", true);
    }

    public void closeDoor()
    {

        Debug.Log("작동!");
        ani.SetBool("isopen", false);
         Debug.Log("작동완료!");


     

    }
}
