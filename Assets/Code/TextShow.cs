using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour {

	public Text InputText; 
	public GameObject WellUI;

	void Start(){

		WellUI.SetActive (false);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{

		if (other.CompareTag ("Player")) 
		{
			WellUI.SetActive (true);
			InputText.text = ("There is no " +
				"water in well");
		}
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			WellUI.SetActive (true);
			InputText.text = ("There is no " +
				"water in well");
		}

	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			WellUI.SetActive (false);
			InputText.text = ("");
		}

	}
}
