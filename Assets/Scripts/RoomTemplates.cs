using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;             //막힌 방 프리팹

    // 생성된 방의 위치 추적
    public HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();

}
