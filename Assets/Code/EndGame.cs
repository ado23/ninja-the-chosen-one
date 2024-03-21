using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public string FirstLevel;

	void Update () {

		if (!Input.GetKey (KeyCode.Return))
			return;

		GameManager.Instance.Reset ();
		SceneManager.LoadScene(FirstLevel);
	}
}