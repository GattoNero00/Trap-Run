using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float grav = 20; //重力数値
    [SerializeField] private float movePower = 9; //X方向の運動量
    [SerializeField] private float jumpMovePower = 7; //空中移動速度
    [SerializeField] private float jumpPower = 20; //ジャンプ力
    [SerializeField] private GroundCheck ground; //接地判定
    [SerializeField] private CeilingCheck ceiling; //天井判定


    private Transform tran;
    private Rigidbody2D rigid;
    private PlayerStatus ps;
    private Vector3 scaleVec; //キャラクターが反転した時のScale
    private MoveObject moveObj = null;
    private MoveRound moveRObj = null;
    private string moveFloorTag = "MoveFloor";
    private string moveFloorRTag = "MoveFloorRound";
    private Vector2 addVelocity = Vector2.zero; //動く床の移動量

    private float scaleJudger = 1.6f; //キャラクターが反転した時のScaleの値
    private float velX; //X方向の速度
    private float velY; //Y方向の速度
    private float rotateSpeed = 10f; //回転速度
    private bool isGround = false; //地面に接しているか
    private bool isCeiling = false; //天井にぶつかっているか
    private bool isJump = false; //ジャンプしているか
    private bool isJumpingCheck = true;
    public bool dontMove { set; get; } = false; //動けない状態
    private float jumpTimeCounter;
    private float jumpTime = 0.27f; //ジャンプ入力を受け付ける時間

    private bool pushJump = false; //ジャンプボタン判定
    private bool pushRight = false; //右移動ボタン判定
    private bool pushLeft = false; //左移動ボタン判定

    void Awake()
    {
        tran = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        ps = GetComponent<PlayerStatus>();
        scaleVec = tran.localScale;
        jumpTimeCounter = jumpTime;
        velX = 0;
        velY = 0;

    }

    void Update()
    {
        //テスト用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pushJump = true;
            if (isGround && Time.timeScale != 0)
            {
                AudioManager.Instance.PlaySE("jump06");
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pushJump = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pushRight = true;
            scaleJudger = 1.6f;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            pushRight = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pushLeft = true;
            scaleJudger = -1.6f;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            pushLeft = false;
        }

        if(dontMove && ps.isDead)
        {
            rigid.gravityScale = 10f;
            transform.Rotate(0, 0, rotateSpeed);
        }

    }

    void FixedUpdate()
    {
        addVelocity = Vector2.zero;
        if (moveObj != null)
        {
            addVelocity = moveObj.GetVelocity();
        }

        if (moveRObj != null)
        {
            addVelocity = moveRObj.GetVelocity();
        }

        //接地判定
        isGround = ground.IsGround();
        //天井衝突判定
        isCeiling = ceiling.IsCeiling();

        //ジャンプ処理(地面にいるとき）
        if (isGround)
        {
            velY = 0;
            Move();

            if (isJumpingCheck && pushJump)
            {
                jumpTimeCounter = jumpTime;
                isJumpingCheck = false;
                velY = jumpPower;
                isJump = true;
            }
            else
            {
                velY = 0;
                if (!pushJump)
                {
                    isJump = false;
                }

                if (!isJump)
                {
                    Move();
                }
            }
        }

        //空中にいるときのジャンプ処理
        if (isJump)
        {
            jumpTimeCounter -= Time.deltaTime;

            if (pushJump)
            {
                velY -= 0.5f;
                Move();
            }
            else
            {
                velY = 0;
                Move();
            }

            if (jumpTimeCounter < 0)
            {
                isJump = false;
            }

            if (isCeiling)
            {
                isJump = false;
            }
        }

        if (!isGround && !isJump)
        {
            velY = 0;
            Move();
        }

        if (!pushJump)
        {
            isJumpingCheck = true;
        }

    }

    private void Move()
    {
        if (!dontMove)
        {
            //右移動処理
            if (pushRight)
            {
                if (isGround)
                {
                    velX = movePower;
                }
                else
                {
                    velX = jumpMovePower;
                }
                scaleVec.x = scaleJudger;
                tran.localScale = scaleVec;

            }
            //左移動処理
            else if (pushLeft)
            {
                if (isGround)
                {
                    velX = -movePower;
                }
                else
                {
                    velX = -jumpMovePower;
                }

                scaleVec.x = scaleJudger;
                tran.localScale = scaleVec;

            }
            else
            {
                velX = 0;
            }
            rigid.velocity = new Vector2(velX, velY) + addVelocity;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //動く床に乗ったとき
        if (collision.gameObject.tag == moveFloorTag)
        {
            moveObj = collision.gameObject.GetComponent<MoveObject>();
        }
        
        else if (collision.gameObject.tag == moveFloorRTag)
        {
            moveRObj = collision.gameObject.GetComponent<MoveRound>();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == moveFloorTag)
        {
            moveObj = null;
        }
        else if(collision.gameObject.tag == moveFloorRTag)
        {
            moveRObj = null;
        }
    }

    //ジャンプボタンが押されているとき
    public  void PushJumpDown()
    {
        pushJump = true;
        if (isGround && Time.timeScale != 0)
        {
            AudioManager.Instance.PlaySE("jump06");
        }

    }

    //ジャンプボタンが離されたとき
    public void PushJumpUp()
    {
        pushJump = false;
    }

    //右移動ボタンが押されているとき
    public void PushRightDown()
    {
        pushRight = true;
        scaleJudger = 1.6f;
    }
    
    //右移動ボタンが離されたとき
    public void PushRightUp()
    {
        pushRight = false;
    }

    //左移動ボタンが押されているとき
    public void PushLeftDown()
    {
        pushLeft = true;
        scaleJudger = -1.6f;
    }
    
    //左移動ボタンが離されたとき
    public void PushLeftUp()
    {
        pushLeft = false;
    }
}
