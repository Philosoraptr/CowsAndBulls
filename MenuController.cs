using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit ();
	}
}
