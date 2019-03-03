using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayBGM("TitleMovie");
	}
	
	// Update is called once per frame
	void Update () {
		//ゲーム終了
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void StartButtonClicked(){
		AudioManager.Instance.AttachBGMSource.Stop ();
		SceneManager.LoadScene("GameScene_kari");
	}
}
