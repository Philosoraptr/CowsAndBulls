using UnityEngine;
using System.Collections;

public class InstructionsButton : MonoBehaviour {

	public void GoToInstructions(){
		Application.LoadLevel("InstructionsScene");
	}
}
