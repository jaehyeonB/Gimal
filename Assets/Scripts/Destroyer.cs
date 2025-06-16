using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(other.gameObject);
		Invoke("EndDestroy", 4f);
	}

	void EndDestroy()
	{
		Destroy(gameObject);
	}
}
