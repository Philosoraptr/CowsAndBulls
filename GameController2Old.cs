using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Used for GameScene2 - 5 digits
public class GameController2Old : MonoBehaviour {

	public Text checkBtn;

	public GameObject winSceneBull;
	public GameObject resultBullPref;
	public GameObject resultCowPref;
	public GameObject audioPlayer;
	public Transform gamePanel;
	public Transform notificationPanel;
	public Transform resultPanel;
	public Button upBtn1;
	public Button upBtn2;
	public Button upBtn3;
	public Button upBtn4;
	public Button upBtn5;
	public Button downBtn1;
	public Button downBtn2;
	public Button downBtn3;
	public Button downBtn4;
	public Button downBtn5;
	public Button notificationButton;
	public Button submitGuessBtn;
	public Text digit1;
	public Text digit2;
	public Text digit3;
	public Text digit4;
	public Text digit5;
	public Text guesses;
	public float appearWaitTime;
	private int codeLength;
	private int numGuesses;
	private int[] code;
    private int[] guess;
	private int bullCounter;
	private int cowCounter;
	private int zero = 0;
	private string resultsString;
	private bool won;

	public void TestShowCode(){
		checkBtn.text = "";
		for(int i = 0; i < code.Length; i++){
			checkBtn.text += code[i].ToString();
		}
	}

	// Use this for initialization
	void Start (){
		codeLength = 5;
		numGuesses = 0;
		code = new int[codeLength];
		guess = new int[codeLength];
		GenerateCode();
//		won = false;
		CancelInvoke("WinScene");
//		int[] testGuess = new int[4]{3,4,5,7};
//		CheckGuess(testGuess);
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			GoToMenuScene();
		if (Input.touchCount == 1)
			won = false;
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
		guess[0] = int.Parse(digit1.text);
		guess[1] = int.Parse(digit2.text);
		guess[2] = int.Parse(digit3.text);
		guess[3] = int.Parse(digit4.text);
		guess[4] = int.Parse(digit5.text);
		CheckGuess(guess);
	}

	public void CheckGuess(int[] guess){
		ResetVariables();
		for(int i = 0; i < guess.Length; i++){
			for(int j = 0; j < guess.Length; j++){
				if(guess[i] == guess[j]){
					if(i != j){
// Need a message box for the player here
						Debug.Log("You cannot submit the same number twice.");
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
			notificationButtonInstance.transform.localScale = new Vector3(1, 1, 1);
			notificationButtonInstance.transform.localPosition = new Vector3(10, 100, 1);
//			won = true;
			Repeater();
			DisableButtons();
		} else {
			resultsString = " " + resultsString + " | ";
		}
		notificationPanelInstance.GetComponent<Text>().text += resultsString;
		numGuesses += 1;
		guesses.text = string.Concat("Guesses: ", numGuesses);
	}

	void ResetVariables(){
		resultsString = "";
		bullCounter = 0;
		cowCounter = 0;
	}

	void ResetGame(){
		EnableButtons();
		digit1.text = zero.ToString();
		digit2.text = zero.ToString();
		digit3.text = zero.ToString();
		digit4.text = zero.ToString();
		digit5.text = zero.ToString();
		numGuesses = 0;
		guesses.text = string.Concat("Guesses: ", numGuesses);
		foreach(Transform child in resultPanel.transform){
			Destroy(child.gameObject);
		}
	}

	void DisableButtons(){
		upBtn1.interactable = false;
		upBtn2.interactable = false;
		upBtn3.interactable = false;
		upBtn4.interactable = false;
		upBtn5.interactable = false;
		downBtn1.interactable = false;
		downBtn2.interactable = false;
		downBtn3.interactable = false;
		downBtn4.interactable = false;
		downBtn5.interactable = false;
		submitGuessBtn.interactable = false;
	}
	
	void EnableButtons(){
		upBtn1.interactable = true;
		upBtn2.interactable = true;
		upBtn3.interactable = true;
		upBtn4.interactable = true;
		upBtn5.interactable = true;
		downBtn1.interactable = true;
		downBtn2.interactable = true;
		downBtn3.interactable = true;
		downBtn4.interactable = true;
		downBtn5.interactable = true;
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

//	IEnumerator WinScene(){
//		yield return new WaitForSeconds(appearWaitTime * 5);
//
//		GameObject crazyBull = Instantiate(winSceneBull) as GameObject;
//		crazyBull.transform.SetParent(gamePanel);
//		crazyBull.transform.localScale = new Vector3(1f, 1f, 1f);
//		crazyBull.transform.localPosition = new Vector3(62f, 440f, 0f);
//		crazyBull.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f, 50f));
//	}

	void WinScene(){
		GameObject crazyBull = Instantiate(winSceneBull) as GameObject;
		crazyBull.transform.SetParent(gamePanel);
		crazyBull.transform.localScale = new Vector3(1f, 1f, 1f);
		crazyBull.transform.localPosition = new Vector3(62f, 440f, 0f);
		crazyBull.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f, 50f));
	}

	void Repeater(){
		InvokeRepeating("WinScene", appearWaitTime * 5f, 0.2f);
	}

//	void LateUpdate(){
//		if(won)
//			StartCoroutine(WinScene());
//	}
}


