using UnityEngine;
using System.Collections;

public class NotificationButton : MonoBehaviour {

	void Update() {
		if (Input.touchCount == 1) {
			Destroy(this.gameObject);
		}
	}

	public void DestroyButton(){
		Destroy(this.gameObject);
	}
}
