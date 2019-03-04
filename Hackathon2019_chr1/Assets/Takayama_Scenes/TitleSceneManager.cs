using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleSceneManager : MonoBehaviour {

	GameObject arasuji;

	// Use this for initialization
	void Start () {
		AudioManager.Instance.ChangeVolume (0.5f, 1.0f);
		AudioManager.Instance.PlayBGM("TitleMovie");

		arasuji = GameObject.Find ("CanvasArasuji");
		arasuji.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//ゲーム終了
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}

		if (Input.anyKey && !Input.GetKey (KeyCode.Escape)) {
			ArasujiButtonClicked ();
		}
	}

	public void ArasujiButtonClicked(){
		if (!arasuji.activeSelf) {
			AudioManager.Instance.PlaySE ("SELECT");
			arasuji.SetActive (true);
		}
	}
	public void StartButtonClicked(){
		AudioManager.Instance.AttachBGMSource.Stop ();
		AudioManager.Instance.PlaySE ("TITLECALL");
		StartCoroutine ("GoToGameScene");
	}

	IEnumerator GoToGameScene(){
		yield return new WaitForSeconds(2.4f);
		SceneManager.LoadScene("GameScene_kari");
	}
}
