using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour {

	public string LevelName;

	public Text InputText; 

	//int sortingOrder = 1;
	//public SpriteRenderer sprite;
	public AudioClip EndLevel;
	AudioSource audioSource;

	void Start()
	{
		//sprite = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Player> () == null)
			return;

		if (other.CompareTag ("Player")) 
		{
			
			InputText.text = ("Press [E] to enter");
			if (Input.GetKeyDown ("e")) 
			{
				//if (sprite)
				//	sprite.sortingOrder = sortingOrder;
				InputText.text = ("");

				audioSource.volume = 0.05f;
				AudioSource.PlayClipAtPoint (EndLevel, transform.position);
				LevelManager.Instance.GoToNextLevel (LevelName);
				Destroy (other);
			}
		}
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			
			if (Input.GetKeyDown ("e")) 
			{
				//if (sprite)
				//	sprite.sortingOrder = sortingOrder;
				InputText.text = ("");

				audioSource.volume = 0.05f;
				AudioSource.PlayClipAtPoint (EndLevel, transform.position);
				LevelManager.Instance.GoToNextLevel (LevelName);
				Destroy (other);
			}
		}
			
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			//if (sprite)
			//	sprite.sortingOrder = 0;
			InputText.text = ("");
		}
		
	}
	
}