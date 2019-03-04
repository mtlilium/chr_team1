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
	}

	public void ArasujiButtonClicked(){
		if (!arasuji.activeSelf) {
			arasuji.SetActive (true);
		}
	}
	public void StartButtonClicked(){
		AudioManager.Instance.AttachBGMSource.Stop ();
		SceneManager.LoadScene("Stage");
	}
}
