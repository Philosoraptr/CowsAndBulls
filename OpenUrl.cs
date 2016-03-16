using UnityEngine;
using System.Collections;

public class OpenUrl : MonoBehaviour {
	public void OpenUrlString(string Url) {
		Application.OpenURL(Url);
	}
}
