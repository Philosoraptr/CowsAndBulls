using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			BackToMenu();
	}

	public void BackToMenu(){
		Application.LoadLevel("MenuScene");
	}
}
