using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit ();
	}
}
