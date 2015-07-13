using UnityEngine;
using System.Collections;

public class InstructionsBackButton : MonoBehaviour {

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			BackToMenu();
	}

	public void BackToMenu(){
		Application.LoadLevel("MenuScene");
	}
}
