using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class GuessButton {
	public Button upBtn;
	public Button downBtn;
	public Text digit;

	public GuessButton(Button up, Button down, Text digitText) {
		upBtn = up;
		downBtn = down;
		digit = digitText;
	}
}
