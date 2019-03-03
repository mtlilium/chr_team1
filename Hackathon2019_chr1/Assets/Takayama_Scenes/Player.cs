using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Player : MonoBehaviour {

	public float jumpForce;       // ジャンプ時に加える力
	public float jumpThreshold;    // ジャンプ中か判定するための閾値
	public float runForce;       // 走り始めに加える力
	public float runSpeed;       // 走っている間の速度
	public float runThreshold;   // 速度切り替え判定のための閾値
	public bool isGround;        // 地面と接地しているか管理するフラグ
	public int key;                 // 左右の入力管理

	public string state;                // プレイヤーの状態管理
	string prevState;            // 前の状態を保存
	float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数

	private float HP; //モチベ。
	private int score; //スコア
	private GameObject image;

	Rigidbody2D rb2;

	// Use this for initialization
	void Start () {
		isGround = true;        // 地面と接地しているか管理するフラグ
		key = 0;  
		rb2 = this.GetComponent<Rigidbody2D> ();
		HP = 100; //初期化
		score = 0;
		image = GameObject.Find("ImagePlayer");
		this.transform.position = new Vector2 (0, 0);
		RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up);
		this.transform.position = hit.point;
	}
	
	// Update is called once per frame
	void Update () {
		GetInputKey ();          //  入力を取得
		ChangeState ();          //  状態を変更する
		Move ();                 //  入力に応じて移動する
		HP_Manage(); //HP制御
	}

	private void GetInputKey(){
		key = 0;
		if(Input.GetKey (KeyCode.RightArrow)){
			image.transform.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
			key = 1;
		}
		if(Input.GetKey (KeyCode.LeftArrow)){
			image.transform.rotation = Quaternion.Euler (0.0f, 180.0f, 0.0f);
			key = -1;
		}
	}

	private void ChangeState(){
		/* 空中にいるかどうかの判定
		上下の速度(rigidbody.velocity)が
		一定の値を超えている場合、
		空中とみなす*/
		if (Mathf.Abs (rb2.velocity.y) > jumpThreshold) {
			isGround = false;
		}

		//接地
		//走っているか何もしてないか
		if (isGround) {
			//走っている
			if (key != 0) {
				state = "RUN";
			} else {
				state = "IDLE";
			}
		} else {
			//空中
			//上昇中or下降中
			if (rb2.velocity.y > 0) {
				state = "JUMP";
			} else if (rb2.velocity.y < 0) {
				state = "FALL";
			}
		}
	}

	private void Move(){
		
		// 設置している時にSpaceキー押下でジャンプ
		if (isGround) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				//ジャンプ音
				//AudioManager.Instance.PlaySE("UEI");
				//ジャンプするのは疲れるので
				HP -= 3.0f;
				rb2.AddForce (transform.up * this.jumpForce);
				isGround = false;
			}
		}

		// 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
		float speedX = Mathf.Abs (rb2.velocity.x);
		//Debug.Log (key + "Miria");
		if (speedX < this.runThreshold) {
			rb2.AddForce (transform.right * key * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
		} else {
			this.transform.localPosition += new Vector3 (runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
		}

	}

	//HP何をせずとも減っていって、0になるとゲームオーバー
	void HP_Manage(){
		HP -= 0.05f;
		if (HP <= 0) {
			HP = 0;
			//Destroy (this.gameObject);
		}
	}

	//HP情報の取得
	public float GetHP(){
		return HP;
	}

	//HP回復
	public void Heal_HP(int point){
		HP += point;
		if (HP >= 100) {
			HP = 100;
		}
	}

	//スコア追加
	public void Add_Score(int add){
		score += add;
	}
	//score情報取得
	public int GetScore(){
		return score;
	}

	//着地判定
	void OnCollisionEnter2D(Collision2D col){
		//print ("入ったよ");
		if (col.gameObject.tag == "Ground") {
			if (!isGround)
				//Debug.Log (isGround + "Akagi");
				isGround = true;
		}

		//敵衝突判定
		if (col.gameObject.tag == "Enemy") {
			HP -= 25; //col.gameObject.damage
		}
	}
	void OnCollisionStay2D(Collision2D col){
		//print ("みりあ入ったよ");
		if (col.gameObject.tag == "Ground") {
			//Debug.Log (isGround + "Miria");
			if(!isGround)
				isGround = true;
		}
	}
}
