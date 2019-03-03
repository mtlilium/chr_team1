using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float acceleration = 5;
    public float maxSpeed = 5;
    Rigidbody2D r2d;
    public Transform jumpAttackJudgementBorder;
    bool isDead = false;
    public SpriteRenderer sprite;

    public float direction;
    public float rayDistance = 1;
    public Vector2 rayStartOffset = new Vector2(0, -1);

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        StartCoroutine(FlipAnimation());
        direction = -1;
    }

    private void Update()
    {
        //debug用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Kill();
        }

        var hits = Physics2D.RaycastAll(transform.position + new Vector3(rayStartOffset.x, rayStartOffset.y, 0), new Vector2(direction, 0), distance: rayDistance);
        Debug.DrawRay(transform.position + new Vector3(rayStartOffset.x, rayStartOffset.y, 0), new Vector2(direction, 0) * rayDistance, Color.red);
        if (hits.Length != 0)
        {
            foreach (var hit in hits)
            {
                //print(hit.collider.name);
                if (hit.collider.tag == "Ground")
                {
                    var rot = transform.rotation.eulerAngles;
                    rot.y += 180;
                    transform.eulerAngles = rot;
                    direction = -direction;
                }
            }
            //print(hit2d.collider.tag);
            //print(hit2d.collider.name);
        }

        ////回転を制限
        //var lrot = transform.rotation;
        //lrot.z = 0;
        //transform.rotation = lrot;

        //r2d.
        //r2d.rotation = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    IEnumerator FlipAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            sprite.flipX = !sprite.flipX;
        }
    }

    void Move()
    {
        if (r2d.velocity.x < maxSpeed)
        {
            r2d.AddForce(Vector2.right * direction * acceleration);
        }
        //print(r2d.velocity.x);
        if (Mathf.Abs(r2d.velocity.x) > maxSpeed)
        {
            var tmpV = r2d.velocity;
            tmpV.x = maxSpeed * direction;
            r2d.velocity = tmpV;
        }

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
            }
        }
    }
}
