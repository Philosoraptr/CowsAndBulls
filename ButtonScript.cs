using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	public Text guessText;

	public void IncreaseValue(){
		int value;
		value = int.Parse(guessText.GetComponent<Text>().text);
		if(value < 9){
			value += 1;
		}
		guessText.text = value.ToString();
	}

	public void DecreaseValue(){
		int value;
		value = int.Parse(guessText.GetComponent<Text>().text);
		if(value > 0){
			value -= 1;
		}
		guessText.text = value.ToString();
	}
}
