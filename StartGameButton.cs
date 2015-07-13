using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour {

	public void GoToGameScene(){
		Application.LoadLevel("GameScene");
	}
}
