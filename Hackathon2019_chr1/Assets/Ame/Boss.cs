using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //プレイヤーを取得
    Player player;

    public float acceleration = 5;
    public float maxSpeed = 5;
    Rigidbody2D r2d;
    public Transform jumpAttackJudgementBorder;
    bool isDead = false;
    public SpriteRenderer sprite;
    public Collider2D collider;

    public int direction;
    public float rayDistance = 1;

    public int score;

    public int maxDamageForDeath = 8;//死ぬまでに踏む回数
    int currentDamage = 0;
    bool isDamageable = true;

    public float minJumpInterval = 4;
    public float maxJumpInterval = 8;
    public float jumpPower = 8;

    Coroutine flipAnimCoroutine;
    Coroutine randomJumpCoroutine;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        r2d = GetComponent<Rigidbody2D>();
        flipAnimCoroutine = StartCoroutine(FlipAnimation());
        direction = -1;
        randomJumpCoroutine = StartCoroutine(randomMovement());

    }

    private void Update()
    {
        var hits = Physics2D.RaycastAll(transform.position, new Vector2(direction, 0), distance: rayDistance);
        Debug.DrawRay(transform.position, new Vector2(direction, 0) * rayDistance, Color.red);
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
    }

    private void FixedUpdate()
    {
        //プレイヤー左側にいる
        if (player.transform.position.x < transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        Move();
        if (once)
        {
            r2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            once = false;
        }
    }

    bool once = true;

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

    IEnumerator randomMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minJumpInterval, maxJumpInterval));
            r2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(Random.Range(3, 8));
            int sign = Random.Range(0, 1) < 0.5f ? direction : -direction;
            r2d.AddForce(Vector2.right * sign * 3, ForceMode2D.Impulse);
        }
    }

    public void Kill()
    {
        if (isDead)
            return;

        isDead = true;
        this.player.Add_Score(score);
        //アニメーションとか死亡処理とか入れる
        collider.enabled = false;
        r2d.constraints = RigidbodyConstraints2D.None;

        StopCoroutine(randomJumpCoroutine);
        StopCoroutine(flipAnimCoroutine);
        var rot = sprite.transform.rotation.eulerAngles;
        rot.z = -90;
        sprite.transform.eulerAngles = rot;

        Destroy(gameObject, 10);
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
                TakeDamage();
            }
            //そのまま当たった時
            //プレイヤー倒す
        }
    }




    void TakeDamage()
    {
        print("ダメージ");

        if (!isDamageable)
            return;
        currentDamage += 1;
        if (currentDamage >= maxDamageForDeath)
        {
            Kill();
        }
        //r2d.AddForce(Vector2.right, ForceMode2D.Impulse);
        r2d.AddForce(new Vector2(100, 5), ForceMode2D.Impulse);
        print("owari");
        //StartCoroutine(InvincibleTime());
    }

    //IEnumerator InvincibleTime()
    //{
    //    print("無敵タイム");
    //    var coroutine = StartCoroutine(AnimationFlashing());
    //    isDamageable = false;
    //    yield return new WaitForSeconds(1);

    //    isDamageable = true;
    //    StopCoroutine(coroutine);
    //    print("stop");
    //}

    //IEnumerator AnimationFlashing()
    //{
    //    while (true)
    //    {
    //        sprite.enabled = !sprite.enabled;
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}
}
