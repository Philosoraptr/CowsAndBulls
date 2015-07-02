﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public Text checkBtn;

	public GameObject resultBullPref;
	public GameObject resultCowPref;
	public Transform resultPanel;
	public Button upBtn1;
	public Button upBtn2;
	public Button upBtn3;
	public Button upBtn4;
	public Button downBtn1;
	public Button downBtn2;
	public Button downBtn3;
	public Button downBtn4;
	public Button submitGuessBtn;
	public Text digit1;
	public Text digit2;
	public Text digit3;
	public Text digit4;
	public Text resultsText;
	private int codeLength;
	private int numGuesses;
	private int[] code;
    private int[] guess;
	private int bullCounter;
	private int cowCounter;
	private string cowResult;
	private string resultsString;
	int zero = 0;

	public void TestShowCode(){
		checkBtn.text = "";
		for(int i = 0; i < code.Length; i++){
			checkBtn.text += code[i].ToString();
		} 
	}

	// Use this for initialization
	void Start () {
		codeLength = 4;
		numGuesses = 0;
		code = new int[codeLength];
		guess = new int[codeLength];
		GenerateCode();
//		int[] testGuess = new int[4]{3,4,5,7};
//		CheckGuess(testGuess);
	}

	public void GenerateCode(){
		ResetVariables();
		ResetGame();
		for(int i = 0; i < code.Length; i++){
			code[i] = GenerateUniqueRandomInt(0, 10, code);
			Debug.Log(code[i]);
		}
	}

	public int GenerateUniqueRandomInt(int min, int max, int[] arr) {
		int result = Random.Range(min, max);
		for(int i = 0; i < arr.Length; i++){
			if(result == arr[i]) {		
				return GenerateUniqueRandomInt(min, max, arr);
			}
		}
		return result;
	}

	public void SetGuess(){
		guess[0] = int.Parse(digit1.text);
		guess[1] = int.Parse(digit2.text);
		guess[2] = int.Parse(digit3.text);
		guess[3] = int.Parse(digit4.text);
		CheckGuess(guess);
	}

	public void CheckGuess(int[] guess){
		float cowX = -120f;
		float cowY = 142f;
		ResetVariables();
		for(int i = 0; i < guess.Length; i++){
			for(int j = 0; j < guess.Length; j++){
				if(guess[i] == guess[j]){
					if(i != j){
						Debug.Log("You cannot submit the same number twice.");
						return;
					}
				}
			}
		}
		for(int i = 0; i < guess.Length; i++){
			for(int j = 0; j < code.Length; j++){
				if(guess[i] == code[j]){
					if(i == j){
						bullCounter += 1;
					} else {
						cowCounter += 1;
					}
				}
			}
		}
		if(bullCounter > 0){
			for(int i = 0; i < bullCounter; i++){
//				cowResult = cowResult + "Bull ";
				GameObject resultImageInstance = Instantiate(resultBullPref) as GameObject;
				resultImageInstance.transform.SetParent(resultPanel);
				resultImageInstance.transform.localScale = new Vector3(1, 1, 1);
				resultImageInstance.transform.position = new Vector2(resultPanel.position.x + cowX + (i * 35f), resultPanel.position.y + cowY - (numGuesses * 30f));
			}
		}
		if(cowCounter > 0){
			for(int i = 0; i < cowCounter; i++){
//				cowResult = cowResult + "Cow ";	
				GameObject resultImageInstance = Instantiate(resultCowPref) as GameObject;
				resultImageInstance.transform.SetParent(resultPanel);
				resultImageInstance.transform.localScale = new Vector3(1, 1, 1);
				resultImageInstance.transform.position = new Vector2(resultPanel.position.x + cowX + (bullCounter * 35f) + (i * 35f), resultPanel.position.y + cowY - (numGuesses * 30f));
			}
		}
		for(int i = 0; i < guess.Length; i++){
			resultsString += guess[i].ToString();
		}
		if(bullCounter == 4){
			resultsString = resultsString + " | " + cowResult + "You Win!!";
			DisableButtons();
		} else {
			resultsString = resultsString + " | " + cowResult + "\n";
		}
		resultsText.text += resultsString;
		numGuesses += 1;
	}

	void ResetVariables(){
		cowResult = "";
		resultsString = "";
		bullCounter = 0;
		cowCounter = 0;
	}

	void ResetGame(){
		EnableButtons();
		resultsText.text = resultsString;
		digit1.text = zero.ToString();
		digit2.text = zero.ToString();
		digit3.text = zero.ToString();
		digit4.text = zero.ToString();
		numGuesses = 0;
	}

	void DisableButtons(){
		upBtn1.interactable = false;
		upBtn2.interactable = false;
		upBtn3.interactable = false;
		upBtn4.interactable = false;
		downBtn1.interactable = false;
		downBtn2.interactable = false;
		downBtn3.interactable = false;
		downBtn4.interactable = false;
		submitGuessBtn.interactable = false;
	}
	
	void EnableButtons(){
		upBtn1.interactable = true;
		upBtn2.interactable = true;
		upBtn3.interactable = true;
		upBtn4.interactable = true;
		downBtn1.interactable = true;
		downBtn2.interactable = true;
		downBtn3.interactable = true;
		downBtn4.interactable = true;
		submitGuessBtn.interactable = true;
	}
}


