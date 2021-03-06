﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Item_Coin : MonoBehaviour {

	Player player;
	GameSceneManager gcm;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player> ();
		gcm = GameObject.Find ("GameSceneManager").GetComponent<GameSceneManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			if (transform.name == "Coin(Clone)" || transform.name == "Coin") {
				AudioManager.Instance.PlaySE ("GETCOIN");
				player.Heal_HP (5);
				player.Add_Score (500);
			} else if (transform.name == "RedCoin(Clone)" || transform.name == "RedCoin") {
				AudioManager.Instance.PlaySE ("GETCOIN");
				player.Heal_HP (10);
				player.Add_Score (1000);
			} else if (transform.name == "Yuri(Clone)" || transform.name == "Yuri") {
				AudioManager.Instance.PlaySE ("KMS");
				player.Heal_HP (30);
				player.Add_Score (5000);
			} else if (transform.name == "Sushi(Clone)" || transform.name == "Sushi") {
				AudioManager.Instance.PlaySE ("SUSHI");
				gcm.AddTime (10);
				player.Add_Score (3000);
			}
			Destroy (this.gameObject);
		}
	}
}
