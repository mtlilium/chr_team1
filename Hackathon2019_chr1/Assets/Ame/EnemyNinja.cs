using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinja : MonoBehaviour
{
    public float torque;

	//プレイヤーを取得
	Player player;

	private bool flag;
    Rigidbody2D r2d;

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

	private void Start(){
		player = GameObject.Find ("Player").GetComponent<Player> ();
		flag = false;
	}

	private void Update(){
		if (!flag && Mathf.Abs (player.transform.position.x - this.transform.position.x) <= 30) {
			flag = true;
		}
	}
    private void FixedUpdate()
    {
		if (flag) {
			r2d.AddTorque (torque);
		}
    }
}
