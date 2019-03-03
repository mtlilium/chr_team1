using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ShowTexts : MonoBehaviour {
	
	Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = "HP = " + player.GetHP ().ToString();
	}
}
