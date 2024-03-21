using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour {

	public string LevelName;
	public float levelStartDelay=0.1f;

	void Update () {
	Invoke("SceneTransition", levelStartDelay);
	}

	void SceneTransition(){
		SceneManager.LoadScene(LevelName);
	}
}
