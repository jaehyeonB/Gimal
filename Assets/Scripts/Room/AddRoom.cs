using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        // 두 RoomSpawner가 겹칠 경우 하나만 살아남도록
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>() != null && GetComponent<RoomSpawner>() != null)
            {
                if (!other.GetComponent<RoomSpawner>().spawned && !GetComponent<RoomSpawner>().spawned)
                {
                    // 충돌 시 둘 다 아직 생성 안 했으면 하나 제거
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
