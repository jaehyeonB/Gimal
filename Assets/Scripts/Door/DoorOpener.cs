using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    public void OpenDoor()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("isopen", true);
    }

    public void CloseDoor()
    {
        GetComponent <BoxCollider2D>().enabled = true;
        animator.SetBool("isopen", false);
    }
}
