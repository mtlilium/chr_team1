using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	Player player;

	Text hpText;
	Text scoreText;
	Text timeText;

	//時間制限
	float timeLimit;

	//ゲームクリアflag
	public static bool isClear;
	GameObject gameClearPic;

	//ゲームオーバーlag
	public static bool isGameOver;
	GameObject gameOverPic;

	//記録用のスコア値static
	public static int baseScore;
	public static int clearBonus;

	//Slider
	Slider hpSlider;
	Slider scoreSlider;

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM("BGM(WAHU)");
		player = GameObject.Find("Player").GetComponent<Player> ();

		hpText = GameObject.Find ("TextHP").GetComponent<Text> ();
		scoreText = GameObject.Find ("TextScore").GetComponent<Text> ();
		timeText = GameObject.Find ("TextTime").GetComponent<Text> ();

		gameClearPic = GameObject.Find ("ImageGameClear");
		gameClearPic.SetActive (false);
		gameOverPic = GameObject.Find ("ImageGameOver");
		gameOverPic.SetActive (false);

		hpSlider = GameObject.Find ("HPSlider").GetComponent<Slider> ();

		timeLimit = 121.0f; //時間制限初期化
		isClear = false;
		isGameOver = false;

		baseScore = 0;
		clearBonus = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (!isClear && !isGameOver) {
			timeLimit -= Time.deltaTime * 2;
		}
		if (timeLimit <= 0) {
			timeLimit = 0;
		}
		if (!isClear && !isGameOver) {
			ShowHP ();
			ShowTime ();
			ShowScore ();
		}
		if (isGameOver) {
			GameOver ();
		}
		if (isClear) {
			GameClear ();
		}
	}

	private void ShowHP(){
		if (!isClear && !isGameOver) {
			hpSlider.value = player.GetHP () / 100f;
		}
		hpText.text = "モチベ " + ((int)player.GetHP ()).ToString() + "%";

		if (player.GetHP () <= 0 && !isGameOver) {
			isGameOver = true;
		}
	}

	private void ShowScore(){
		scoreText.text = "Score = " + player.GetScore ().ToString ();

		//debug you
		if (player.GetScore () >= 10000 && !isClear) {
			print ("a");
			isClear = true;
		}
	}

	private void ShowTime(){
		timeText.text = "TimeLimit = " + ((int)(timeLimit/60)).ToString () + " : " + ((int)(timeLimit%60)).ToString ();

		if (timeLimit <= 0 && !isGameOver) {
			isGameOver = true;
		}
	}

	//時間延長
	public void AddTime(int add){
		timeLimit += add;
	}

	//音は一回のみ
	bool flag = true;
	//ゲームオーバー処理
	private void GameOver(){
		AudioManager.Instance.AttachBGMSource.Stop ();
		baseScore = player.GetScore ();
		clearBonus = 0;
		player.toCantMove ();
		if (flag) {
			gameOverPic.SetActive (true);
			AudioManager.Instance.PlaySE ("DEAD");
			flag = false;
		}
		StartCoroutine ("GoToResultScene");
	}

	//ゲームクリア処理
	private void GameClear(){
		AudioManager.Instance.AttachBGMSource.Stop ();
		baseScore = player.GetScore ();
		clearBonus = (int)(timeLimit * player.GetHP () * 10);
		if (!gameClearPic.activeSelf) {
			gameClearPic.SetActive (true);
		}
		player.toCantMove ();
		StartCoroutine ("GoToResultScene");
	}

	//リザルト画面に遷移
	IEnumerator GoToResultScene(){
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("ResultScene");
	}
}
