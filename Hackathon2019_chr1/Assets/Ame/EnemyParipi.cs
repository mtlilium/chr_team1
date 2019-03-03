using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParipi : MonoBehaviour
{
    //プレイヤーを取得
    Player player;


    Rigidbody2D r2d;
    public Transform jumpAttackJudgementBorder;
    bool isDead = false;



    public int score;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        r2d = GetComponent<Rigidbody2D>();
    }


    public void Kill()
    {
        if (isDead)
            return;

        isDead = true;
        //アニメーションとか死亡処理とか入れる
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("プレイヤーと接触");
            //上から踏まれた処理
            var hitPositions = collision.contacts;
            //print(hitPositions[0].point.y);
            var player = collision.gameObject;


            if (player.transform.position.y > jumpAttackJudgementBorder.position.y)
            {
                Kill();
                this.player.Add_Score(score);
            }
            //そのまま当たった時
            //プレイヤー倒す
        }
    }
}
