using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject DestroyedEffect;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger == true) 
		{
			if (col.CompareTag ("Player") || col.CompareTag("Rocks")) {

				Instantiate (DestroyedEffect, transform.position, transform.rotation);
				Destroy (gameObject);
		
			}
		}

			
	}


}