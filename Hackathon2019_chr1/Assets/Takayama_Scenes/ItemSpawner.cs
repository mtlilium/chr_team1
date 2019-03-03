using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ItemSpawner : MonoBehaviour {

	public GameObject coinPrefab;
	public GameObject redCoinPrefab;
	public GameObject yuriPrefab;
	public GameObject sushiPrefab;

	// Use this for initialization
	void Start () {
		//yuriの生成
		for (int i = 1; i <= 15; i++) {
			int tmp = (int)UnityEngine.Random.Range (10, 40);
			if (tmp >= 25) {
				Vector3 offset = new Vector3 (UnityEngine.Random.Range (30, 45) * i, 0.0f, 0.0f);
				RaycastHit2D hit = Physics2D.Raycast (transform.position + offset, -Vector2.up);
				hit.point = new Vector2 (hit.point.x, hit.point.y + 1);
				if (hit.collider != null) {
					GameObject obj = Instantiate (yuriPrefab, hit.point, transform.rotation);
					obj.transform.parent = transform;
				}
			}
		}
		//寿司の生成
		for (int i = 1; i <= 15; i++) {
			int tmp = (int)UnityEngine.Random.Range (10, 40);
			if (tmp >= 20) {
				Vector3 offset = new Vector3 (UnityEngine.Random.Range (30, 45) * i, 0.0f, 0.0f);
				RaycastHit2D hit = Physics2D.Raycast (transform.position + offset, -Vector2.up);
				hit.point = new Vector2 (hit.point.x, hit.point.y + 1);
				if (hit.collider != null) {
					GameObject obj = Instantiate (sushiPrefab, hit.point, transform.rotation);
					obj.transform.parent = transform;
				}
			}
		}
		//黄色コインの生成
		for (int i = 1; i <= 30; i++) {
			Vector3 offset = new Vector3 (UnityEngine.Random.Range (18, 25) * i,0.0f, 0.0f);
			RaycastHit2D hit = Physics2D.Raycast (transform.position + offset, -Vector2.up);
			hit.point = new Vector2 (hit.point.x, hit.point.y + UnityEngine.Random.Range (3, 6));
			if (hit.collider != null) {
				GameObject obj = Instantiate (coinPrefab, hit.point, transform.rotation);
				obj.transform.parent = transform;
			}
		}
		//赤色コインの生成
		for (int i = 1; i <= 15; i++) {
			Vector3 offset = new Vector3 (UnityEngine.Random.Range (40, 50) * i,0.0f, 0.0f);
			RaycastHit2D hit = Physics2D.Raycast (transform.position + offset, -Vector2.up);
			hit.point = new Vector2 (hit.point.x, hit.point.y + UnityEngine.Random.Range (4, 7));
			if (hit.collider != null) {
				GameObject obj3 = Instantiate (redCoinPrefab, hit.point, transform.rotation);
				obj3.transform.parent = transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
