using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float grav = 20; //�d�͐��l
    [SerializeField] private float movePower = 9; //X�����̉^����
    [SerializeField] private float jumpMovePower = 7; //�󒆈ړ����x
    [SerializeField] private float jumpPower = 20; //�W�����v��
    [SerializeField] private GroundCheck ground; //�ڒn����
    [SerializeField] private CeilingCheck ceiling; //�V�䔻��


    private Transform tran;
    private Rigidbody2D rigid;
    private PlayerStatus ps;
    private Vector3 scaleVec; //�L�����N�^�[�����]��������Scale
    private MoveObject moveObj = null;
    private MoveRound moveRObj = null;
    private string moveFloorTag = "MoveFloor";
    private string moveFloorRTag = "MoveFloorRound";
    private Vector2 addVelocity = Vector2.zero; //�������̈ړ���

    private float scaleJudger = 1.6f; //�L�����N�^�[�����]��������Scale�̒l
    private float velX; //X�����̑��x
    private float velY; //Y�����̑��x
    private float rotateSpeed = 10f; //��]���x
    private bool isGround = false; //�n�ʂɐڂ��Ă��邩
    private bool isCeiling = false; //�V��ɂԂ����Ă��邩
    private bool isJump = false; //�W�����v���Ă��邩
    private bool isJumpingCheck = true;
    public bool dontMove { set; get; } = false; //�����Ȃ����
    private float jumpTimeCounter;
    private float jumpTime = 0.27f; //�W�����v���͂��󂯕t���鎞��

    private bool pushJump = false; //�W�����v�{�^������
    private bool pushRight = false; //�E�ړ��{�^������
    private bool pushLeft = false; //���ړ��{�^������

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
        //�e�X�g�p
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

        //�ڒn����
        isGround = ground.IsGround();
        //�V��Փ˔���
        isCeiling = ceiling.IsCeiling();

        //�W�����v����(�n�ʂɂ���Ƃ��j
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

        //�󒆂ɂ���Ƃ��̃W�����v����
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
            //�E�ړ�����
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
            //���ړ�����
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
        //�������ɏ�����Ƃ�
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

    //�W�����v�{�^����������Ă���Ƃ�
    public  void PushJumpDown()
    {
        pushJump = true;
        if (isGround && Time.timeScale != 0)
        {
            AudioManager.Instance.PlaySE("jump06");
        }

    }

    //�W�����v�{�^���������ꂽ�Ƃ�
    public void PushJumpUp()
    {
        pushJump = false;
    }

    //�E�ړ��{�^����������Ă���Ƃ�
    public void PushRightDown()
    {
        pushRight = true;
        scaleJudger = 1.6f;
    }
    
    //�E�ړ��{�^���������ꂽ�Ƃ�
    public void PushRightUp()
    {
        pushRight = false;
    }

    //���ړ��{�^����������Ă���Ƃ�
    public void PushLeftDown()
    {
        pushLeft = true;
        scaleJudger = -1.6f;
    }
    
    //���ړ��{�^���������ꂽ�Ƃ�
    public void PushLeftUp()
    {
        pushLeft = false;
    }
}
