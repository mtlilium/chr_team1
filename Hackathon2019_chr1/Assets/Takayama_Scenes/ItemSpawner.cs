using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ItemSpawner : MonoBehaviour {

	public GameObject coinPrefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 20; i++) {
			Vector3 offset = new Vector3 (10 * i, 0.0f, 0.0f);
			GameObject obj = Instantiate (coinPrefab, transform.position + offset, transform.rotation);
			obj.transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
