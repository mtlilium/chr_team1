using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_teki : MonoBehaviour
{
    public GameObject paripi;

    private int move_type = 0;
    private Vector3 forward;

    private bool isIdle = true;
    private bool isWalk = false;
    private bool isJump = false;

    //jumpparams
    private float jumpPowor;
    public float jumpPoworConst = 0.8f;
    public float jumpGrvity = 0.05f;

    //use this for initialization
    void start()
    {

    }

    //update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            move_type = Random.Range(0, 3);
        }

        else if(isWalk)
        {
            transform.Translate(Vector3.left * 0.1f);
        }
       
        else if (isJump)
        {
            jumpPowor = jumpGrvity;
            transform.Translate(Vector3.up * jumpPowor);
            //地面に着いたら処理終了
            if (jumpPowor < 0 && transform.position.y <= 1)
            {
                isIdle = true;
                isJump = false;
            }
            return;
        }
        else
        {
            return;
        }
        //walk
        if(move_type == 1)
        {
            isIdle = false;
            isWalk = true;
            StartCoroutine("WaitFotwalk");
        }

        // Jump
        if(move_type == 2)
        {
            isIdle = false;
            isJump = true;
            jumpPowor = jumpPoworConst;
        }

    }

    IEnumerator WaitFotwalk()
    {
        yield return new WaitForSeconds(0.5f);
        isIdle = true;
        isWalk = false;
    }


}