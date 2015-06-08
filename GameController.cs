using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public Text digit1;
	public Text digit2;
	public Text digit3;
	public Text digit4;
	public Text resultsText;
	private int codeLength; 
	private int[] code;
    private int[] guess;
	private int bullCounter;
	private int cowCounter;
	private string cowResult;
	private string resultsString;

	// Use this for initialization
	void Start () {
		codeLength = 4;
		code = new int[codeLength];
		guess = new int[codeLength];
//		GenerateCode();
//		int[] testGuess = new int[4]{3,4,5,7};
//		CheckGuess(testGuess);
	}

	public void GenerateCode(){
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
		ResetGame();
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
				cowResult = cowResult + "Bull ";	
			}
		}
		if(cowCounter > 0){
			for(int i = 0; i < cowCounter; i++){
				cowResult = cowResult + "Cow ";	
			}
		}
		cowResult.TrimEnd(' ');
		for(int i = 0; i < guess.Length; i++){
			resultsString += guess[i].ToString();
		}
		resultsString = resultsString + " | " + cowResult + "\n";
		resultsText.text += resultsString;
		if(cowResult == "Bull Bull Bull Bull"){
			resultsText.text += " You win!!"
		}
	}

	void ResetGame(){
		cowResult = "";
		resultsString = "";
		bullCounter = 0;
		cowCounter = 0;
	}
}


