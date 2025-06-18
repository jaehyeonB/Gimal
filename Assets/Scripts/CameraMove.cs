using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform targetPos;

    void Start()
    {
        
        Invoke("moveStart", 5f);
    }

    void Update()
    {
        if (targetPos == null)
            return;

        Vector3 targetPosition = new Vector3(targetPos.position.x, targetPos.position.y, transform.position.z);

        if ((transform.position - targetPosition).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
        }
    }

    public void RoomMoved(Transform target)
    {
        Debug.Log("정상작동!");
        targetPos = target;
    }

    void moveStart()
    {
        transform.position = new Vector3(0, 0, -5);
    }
}
