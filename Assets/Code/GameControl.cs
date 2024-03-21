using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	public GameObject Heart1, Heart2, Heart3, gameOver;
	public GameObject GameOverUI;
	public static int lifes;

	void Start () {

		GameOverUI.SetActive (false);
		lifes = 3;
		Heart1.gameObject.SetActive (true);
		Heart2.gameObject.SetActive (true);
		Heart3.gameObject.SetActive (true);
		gameOver.gameObject.SetActive (false);
		
	}
		
	void Update () {

		if (lifes > 3)
			lifes = 3;

		switch (lifes) {

		case 3:
			Heart1.gameObject.SetActive (true);
			Heart2.gameObject.SetActive (true);
			Heart3.gameObject.SetActive (true);
			break;
		case 2:
			Heart1.gameObject.SetActive (true);
			Heart2.gameObject.SetActive (true);
			Heart3.gameObject.SetActive (false);
			break;
		case 1:
			Heart1.gameObject.SetActive (true);
			Heart2.gameObject.SetActive (false);
			Heart3.gameObject.SetActive (false);
			break;
		case 0:
			Heart1.gameObject.SetActive (false);
			Heart2.gameObject.SetActive (false);
			Heart3.gameObject.SetActive (false);
			gameOver.gameObject.SetActive (true);
			GameOverUI.SetActive (true);

			break;
		}

	}

	public void Restart(){

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		GameManager.Instance.ResetPoints (0);
	}

	public void Quit()
	{
		Application.Quit();
	}
}