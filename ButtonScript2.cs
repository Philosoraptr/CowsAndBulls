using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript2 : MonoBehaviour {
	public Text guessText;
	public Image buttonImage;
	//private AudioSource audioPlayer;
	//private AudioClip clip;

	void Start(){
		//audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		//clip = audioPlayer.clip;
	}

	public void ChangeValue(bool Increase){
		int value;
		string spriteName;
		value = int.Parse(guessText.GetComponent<Text>().text);
		if(Increase){
			if(value < 9){
				value += 1;
			} else if(value == 9){
				value = 0;
			}
		} else if (!Increase) {
			if(value > 0){
				value -= 1;
			} else if(value == 0){
				value = 9;
			}
		}
		guessText.text = value.ToString();
		spriteName = "Images/Current/" + value + " Button";
		buttonImage.sprite = Resources.Load<Sprite>(spriteName);
		//audioPlayer.PlayOneShot(clip);
	}

	public void SetValue(int Value){
		string spriteName;
		guessText.text = Value.ToString();
		spriteName = "Images/Current/" + Value + " Button";
		buttonImage.sprite = Resources.Load<Sprite>(spriteName);
	}
}
