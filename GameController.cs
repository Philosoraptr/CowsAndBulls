using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameController : MonoBehaviour {

	public Text checkBtn;

	public List<GuessButton> guessButtonList = new List<GuessButton>();
	public GameObject winSceneBull;
	public GameObject winSceneCow;
	public GameObject resultBullPref;
	public GameObject resultCowPref;
	public GameObject audioPlayer;
	public Transform gamePanel;
	public Transform notificationPanel;
	public Transform resultPanel;
	public Button notificationButton;
	public Button submitGuessBtn;
	public Text guesses;
	public float appearWaitTime;
	private float gameStartTime;
	private float gameTimeTaken;
	private int gameScore;
	private int codeLength;
	private int numGuesses;
	private int[] code;
    private int[] guess;
	private int bullCounter;
	private int cowCounter;
	private int zero = 0;
	private string resultsString;

	public void TestShowCode(){
		checkBtn.text = "";
		for(int i = 0; i < code.Length; i++){
			checkBtn.text += code[i].ToString();
		}
	}

	void Start (){
		codeLength = guessButtonList.Count;
		numGuesses = 0;
		code = new int[codeLength];
		guess = new int[codeLength];
		GenerateCode();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			GoToMenuScene();
		if (Input.touchCount == 1 || Input.GetMouseButtonDown(0)) 
			CancelInvoke("WinScene"); 
	}

	public void GoToMenuScene(){
		Application.LoadLevel("MenuScene"); 
	}

	public void GenerateCode(){
		ResetVariables();
		ResetGame();
		for(int i = 0; i < code.Length; i++){
			code[i] = GenerateUniqueRandomInt(0, 10, code);
			Debug.Log(code[i]);
		}
	}

	public int GenerateUniqueRandomInt(int min, int max, int[] arr){
		int result = Random.Range(min, max);
		for(int i = 0; i < arr.Length; i++){
			if(result == arr[i]) {		
				return GenerateUniqueRandomInt(min, max, arr);
			}
		}
		return result;
	}

	public void SetGuess(){
		for(int i = 0; i < guessButtonList.Count; i++){
			guess[i] = int.Parse(guessButtonList[i].digit.text);
		}
		CheckGuess(guess);
	}

	public void CheckGuess(int[] guess){
		ResetVariables();
		for(int i = 0; i < guess.Length; i++){
			for(int j = 0; j < guess.Length; j++){
				if(guess[i] == guess[j]){
					if(i != j){
						Button notificationButtonInstance = Instantiate(notificationButton) as Button;
						notificationButtonInstance.GetComponentInChildren<Text>().text = "You may only use each digit once.";
						notificationButtonInstance.transform.SetParent(gamePanel);
						notificationButtonInstance.transform.localScale = new Vector3(1, 1, 1);
						notificationButtonInstance.transform.localPosition = new Vector3(10, 100, 1);
						return;
					}
				}
			}
		}
		numGuesses += 1;
		Transform notificationPanelInstance = Instantiate(notificationPanel) as Transform;
        notificationPanelInstance.transform.SetParent(resultPanel);
		notificationPanelInstance.transform.SetAsFirstSibling();
		notificationPanelInstance.transform.localScale = new Vector3(1, 1, 1);
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
				StartCoroutine(SpawnResultImages(notificationPanelInstance, true, i, i * appearWaitTime));
			}
		}
		if(cowCounter > 0){
			for(int i = 0; i < cowCounter; i++){
				StartCoroutine(SpawnResultImages(notificationPanelInstance, false, i, (bullCounter + i) * appearWaitTime));
			}
		}
		for(int i = 0; i < guess.Length; i++){
			resultsString += guess[i].ToString();
		}
		if(bullCounter == codeLength){
			resultsString = " " + resultsString + " | ";
			Button notificationButtonInstance = Instantiate(notificationButton) as Button;
			notificationButtonInstance.GetComponentInChildren<Text>().text = "You win!";
			notificationButtonInstance.transform.SetParent(gamePanel);
			notificationButtonInstance.transform.localScale = new Vector3(1f, 1f, 1f);
			notificationButtonInstance.transform.localPosition = new Vector3(10f, 100f, 1f);
			Repeater();
			DisableButtons();
			gameTimeTaken = Time.time - gameStartTime;
			Debug.Log("Game time taken: " + gameTimeTaken);
			gameScore = 1000 - (numGuesses * 30) - (int)gameTimeTaken;
			Debug.Log("Game score: " + gameScore);
		} else {
			resultsString = " " + resultsString + " | ";
		}
		notificationPanelInstance.GetComponent<Text>().text += resultsString;
		guesses.text = string.Concat("Guesses: ", numGuesses);
	}
// This is used to reset variables used between guesses
	void ResetVariables(){
		resultsString = "";
		bullCounter = 0;
		cowCounter = 0;
	}
// This is used to reset variables used between games
	void ResetGame(){
		EnableButtons();
		DestroyResults();
		for(int i = 0; i < guessButtonList.Count; i++){
			guessButtonList[i].digit.text = zero.ToString();
		}
		numGuesses = 0;
		guesses.text = string.Concat("Guesses: ", numGuesses);
		gameStartTime = Time.time;
		Debug.Log("Start time:" + gameStartTime);
	}

	void DestroyResults(){
		foreach(Transform child in resultPanel.transform){
			Destroy(child.gameObject);
		}
	}

	void DisableButtons(){
		for(int i = 0; i < guessButtonList.Count; i++){
			guessButtonList[i].upBtn.interactable = false;
			guessButtonList[i].downBtn.interactable = false;
		}
		submitGuessBtn.interactable = false;
	}
	
	void EnableButtons(){
		for(int i = 0; i < guessButtonList.Count; i++){
			guessButtonList[i].upBtn.interactable = true;
			guessButtonList[i].downBtn.interactable = true;
		}
		submitGuessBtn.interactable = true;
	}

	IEnumerator SpawnResultImages(Transform panel, bool bull, int i, float waitTime){
		float xPos = 200f;
		float yPos = 0f;
		float xSpacer = 80f;
		float ySpacer = 0f;
		string animName;
		yield return new WaitForSeconds(waitTime);
		if(bull){
			GameObject resultImageInstance = Instantiate(resultBullPref) as GameObject;
			resultImageInstance.transform.SetParent(panel);
			resultImageInstance.transform.localScale = new Vector3(1, 1, 1);
			resultImageInstance.transform.localPosition = new Vector2(xPos + (i * xSpacer), yPos - ((numGuesses - 1) * ySpacer));
			animName = string.Concat("BullAppear", Random.Range(1, 4));
			resultImageInstance.GetComponent<Animator>().Play (animName);
			audioPlayer.GetComponent<AudioPlayer>().PlayRandomMoo();
		} else {
			GameObject resultImageInstance = Instantiate(resultCowPref) as GameObject;
			resultImageInstance.transform.SetParent(panel);
			resultImageInstance.transform.localScale = new Vector3(1, 1, 1);
			resultImageInstance.transform.localPosition = new Vector2(xPos + (bullCounter * xSpacer) + (i * xSpacer), yPos - ((numGuesses - 1) * ySpacer));
			animName = string.Concat("CowAppear", Random.Range(1, 4));
			resultImageInstance.GetComponent<Animator>().Play (animName);
			audioPlayer.GetComponent<AudioPlayer>().PlayRandomMoo();
		}
	}

	void WinScene(){
		DestroyResults();
		for(int i = 0; i < codeLength; i++){
			int rand = Random.Range(0, 2);
			if(rand == 0){
				SpawnRigidCows(winSceneBull, i);
			} else if(rand == 1){
				SpawnRigidCows(winSceneCow, i);
			}
		}
	}
	
	void Repeater(){
		InvokeRepeating("WinScene", appearWaitTime * 5f, 0.1f);
	}

	void SpawnRigidCows(GameObject cowBull, int step){
		float xPos = -258f;
		float yPos = 440f;
		float xSpacer = 80f;
		GameObject rigidCow = Instantiate(cowBull) as GameObject;
		rigidCow.transform.SetParent(gamePanel);
		rigidCow.transform.localScale = new Vector3(1f, 1f, 1f);
		rigidCow.transform.localPosition = new Vector3(xPos + (step * xSpacer), yPos, 0f);
		rigidCow.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f, 50f));
	}
}


