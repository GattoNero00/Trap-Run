using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class FallFloor : MonoBehaviour
{
    [SerializeField] private GameObject spriteObj;
    [SerializeField] private float fallTime = 1.0f; //落下するまでの時間
    [SerializeField, Range(0f,20f)] private float fallSpeed = 10.0f; //落下速度
    [SerializeField] private float returnTime = 5.0f; //床が元の位置に戻るまでの時間

    private bool isOnCharacter; //プレイヤーの足が触れたかどうか
    private bool isOn = false; //乗っているかどうか
    private bool isFall = false; //落下しているかどうか
    private bool isReturn; //リスポーンするかどうか
    private Vector3 floorDefautPos;
    private Vector2 fallVelocity;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerController pc;
    private float timer = 0;
    private float fallingTimer = 0; //落下してからの時間
    private float returnTimer = 0; //リスポーンまでの時間
    private float blinkTimer = 0;  //点滅する時間

    private string groundCheckTag = "GroundCheck";


    void Start()
    {
        rb = spriteObj.GetComponent<Rigidbody2D>();
        sr = spriteObj.GetComponent<SpriteRenderer>();
        floorDefautPos = gameObject.transform.position;
        fallVelocity = new Vector2(0, -fallSpeed);
    }

    private void Update()
    {
        if (isOnCharacter)
        {
            isOn = true;
            isOnCharacter = false;
        }

        if (isOn && !isFall)
        {
            if (timer > fallTime)
            {
                isFall = true;
            }

            timer += Time.deltaTime;
        }

        //戻るときにobjを点滅させる
        if (isReturn)
        {
            if (blinkTimer > 0.2f)
            {
                sr.enabled = true;
                blinkTimer = 0;
            }
            else if (blinkTimer > 0.1f)
            {
                sr.enabled = false;
            }
            else
            {
                sr.enabled = true;
            }
        }

        if (returnTimer > 1.0f)
        {
            isReturn = false;
            blinkTimer = 0;
            returnTimer = 0;
            sr.enabled = true;
        }
        else
        {
            blinkTimer += Time.deltaTime;
            returnTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isFall)
        {
            rb.velocity = fallVelocity;

            //落下から指定時間経過で元の位置に戻る
            if (fallingTimer > returnTime)
            {
                isReturn = true;
                transform.position = floorDefautPos;
                rb.velocity = Vector2.zero;
                isFall = false;
                timer = 0;
                fallingTimer = 0;
            }
            else
            {
                fallingTimer += Time.deltaTime;
                isOn = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == groundCheckTag)
        {
            isOnCharacter = true;
        }
    }
}
