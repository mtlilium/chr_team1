using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour {

	Text bassScoreText;
	Text clearBonusText;
	Text totalScoreText;
	Text commentText;
	Text rankingText;

	//今回のトータルスコア
	int newScore;

	private string RANKING_PREF_KEY = "ScoreRanking";
	private int[] ranking = new int[5]; //5位まで保存

	// Use this for initialization
	void Start () {
		bassScoreText = GameObject.Find ("TextBassScore").GetComponent<Text> ();
		clearBonusText = GameObject.Find ("TextClearBonus").GetComponent<Text> ();
		totalScoreText = GameObject.Find ("TextTotalScore").GetComponent<Text> ();
		commentText = GameObject.Find ("TextComment").GetComponent<Text> ();
		rankingText = GameObject.Find ("TextRanking").GetComponent<Text> ();
		bassScoreText.text = "";
		clearBonusText.text = "";
		totalScoreText.text = "";

		newScore = (GameSceneManager.baseScore + GameSceneManager.clearBonus);
		DisplayScore ();
		GetRanking ();
		SaveRanking (newScore);
	}

	// Update is called once per frame
	void Update () {
		//とりあえずF1キー&R&Yでランキング削除
		if (Input.GetKey (KeyCode.F1) && Input.GetKey (KeyCode.Y) && Input.GetKey (KeyCode.R)) {
			PlayerPrefs.DeleteKey (RANKING_PREF_KEY);
		}
	}

	//スコア表示
	private void DisplayScore(){
		StartCoroutine ("ShowBassScore");
		StartCoroutine ("ShowClearBonus");
		StartCoroutine ("ShowTotalScore");
		StartCoroutine ("ShowRanking");
		StartCoroutine ("StartBGM");
	}

	IEnumerator ShowBassScore(){
		yield return new WaitForSeconds(0.8f);
		AudioManager.Instance.PlaySE("DEN");
		bassScoreText.text = GameSceneManager.baseScore.ToString ();
	}

	IEnumerator ShowClearBonus(){
		yield return new WaitForSeconds(1.6f);
		AudioManager.Instance.PlaySE("DEN");
		clearBonusText.text = GameSceneManager.clearBonus.ToString ();
	}

	IEnumerator ShowTotalScore(){
		yield return new WaitForSeconds(2.4f);
		AudioManager.Instance.PlaySE("DEN");
		totalScoreText.text = newScore.ToString ();
	}

	//ランキングの取得
	private void GetRanking(){
		var _ranking = PlayerPrefs.GetString (RANKING_PREF_KEY);
		if (_ranking.Length > 0) {
			var _score = _ranking.Split ("," [0]);
			ranking = new int[5];
			for (var i = 0; i < _score.Length && i < 5; i++) {
				ranking [i] = int.Parse (_score [i]);
			}
		}
	}
	//ランキングの更新
	private void SaveRanking(int newScore){
		if (ranking.Length > 0) {
			int tmp = 0;
			for (var i = 0; i < ranking.Length; i++) {
				if (ranking [i] < newScore) {
					tmp = ranking [i];
					ranking [i] = newScore;
					newScore = tmp;
				}
			}
		} else {
			ranking [0] = newScore;
		}

		//配列を文字列に変換してPlayerPrefsに格納
		string rankingString = ranking[0].ToString() + "," + ranking[1].ToString() + "," + ranking[2].ToString() + "," + ranking[3].ToString() + "," + ranking[4].ToString();
		PlayerPrefs.SetString (RANKING_PREF_KEY, rankingString);
	}

	//ランキングの表示
	IEnumerator ShowRanking(){
		yield return new WaitForSeconds(3.2f);
		AudioManager.Instance.PlaySE("DEN");

		string rankingString = "";
		for (var i = 0; i < ranking.Length; i++) {
			rankingString = rankingString + (i + 1) + "位  " + ranking[i].ToString() + "\n";
		}
		rankingText.text = rankingString;

		if (GameSceneManager.isClear) {
			if (newScore == ranking [0]) {
				commentText.text = "みりあ";
			} else if (newScore >= ranking [4]) {
				commentText.text = "パリピ";
			} else {
				commentText.text = "オタク";
			}
		} else {
			commentText.text = "†俺は辞退する†";
		}
	}

	IEnumerator StartBGM(){
		yield return new WaitForSeconds(4.0f);
		AudioManager.Instance.PlayBGM("RESULT");
	}

	public void RestartButtonClicked(){
		AudioManager.Instance.AttachBGMSource.Stop ();
		SceneManager.LoadScene("GameScene_kari");
	}
	public void TitleButtonClicked(){
		Destroy (AudioManager.Instance.gameObject);
		SceneManager.LoadScene("TitleScene");
	}
}
