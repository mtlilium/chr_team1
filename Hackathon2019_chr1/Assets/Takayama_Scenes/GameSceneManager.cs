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
	bool isClear;

	//ゲームオーバーlag
	bool isGameOver;

	// Use this for initialization
	void Start () {

		player = GameObject.Find("Player").GetComponent<Player> ();

		hpText = GameObject.Find ("TextHP").GetComponent<Text> ();
		scoreText = GameObject.Find ("TextScore").GetComponent<Text> ();
		timeText = GameObject.Find ("TextTime").GetComponent<Text> ();

		//AudioManager.Instance.PlayBGM("BGM(Select)");

		timeLimit = 121.0f; //時間制限初期化
		isClear = false;
		isGameOver = false;

	}
	
	// Update is called once per frame
	void Update () {
		timeLimit -= Time.deltaTime*2;
		if (timeLimit <= 0) {
			timeLimit = 0;
		}
		ShowHP ();
		ShowScore ();
		ShowTime ();
	}

	private void ShowHP(){
		hpText.text = "HP = " + ((int)player.GetHP ()).ToString();

		if (player.GetHP () <= 0 && !isGameOver) {
			isGameOver = true;
		}
	}

	private void ShowScore(){
		scoreText.text = "Score = " + player.GetScore ().ToString ();
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
}
