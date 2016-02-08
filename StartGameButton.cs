using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour {
	public void GoToGameSceneNormal(){
		Application.LoadLevel("GameScene Normal");
	}
	public void GoToGameSceneHard(){
		Application.LoadLevel("GameScene Hard");
	}
}
