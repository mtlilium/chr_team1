using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Item_Coin : MonoBehaviour {

	Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player> ();
		Debug.Log (player == null ? "miria" : "OK");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		print ("入ったよ");
		if (col.gameObject.tag == "Player") {
			player.Heal_HP (5);
			player.Add_Score (500);
			Destroy (this.gameObject);
		}
	}
}
