using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObject : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoint; //�ړ��ڕW
    [SerializeField] private float speed = 0.5f; //Obj����������
    [SerializeField] private bool canReturn = true; //�������邩�ǂ���
    [SerializeField] private bool isFixedPosition = false; //��Obj�ɏ���Ă���Obj���Œ肷�邩�ǂ���

    private Rigidbody2D rb;
    private int nowPoint = 0; 
    private bool returnPoint = false; //�S�Ă̌o�H��ʉ߂�����
    private bool moveNREnd = false; //MoveNotRetern���Ō�܂œ��B�������ǂ���
    private Vector2 oldPos = Vector2.zero; //�O�̃t���[�����̃|�W�V����
    private Vector2 myVelocity = Vector2.zero; //�����̑��x(�Q�Ɨp)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (movePoint != null && movePoint.Length > 0 && rb != null)
        {
            rb.position = movePoint[0].transform.position;
            oldPos = rb.position;
        }
    }

    private void FixedUpdate()
    {
        if (!canReturn && !moveNREnd)
        {
            MoveNotReturn();
        }
        else if(canReturn)
        {
            MoveNormal();
        }
    }

    //��������Ƃ��̈ړ�����
    private void MoveNormal()
    {
        if (movePoint != null && movePoint.Length > 1 && rb != null)
        {
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    rb.MovePosition(toVector);
                }
                else
                {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
            }
            else
            {
                int nextPoint = nowPoint - 1;


                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    rb.MovePosition(toVector);
                }
                else
                {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;

                    if (nowPoint <= 0)
                    {
                        returnPoint = false;
                    }
                }
            }
            myVelocity = (rb.position - oldPos) / Time.deltaTime;
            oldPos = rb.position;
        }
    }

    //�������Ȃ����̈ړ�����
    private void MoveNotReturn()
    {

        if (movePoint != null && movePoint.Length > 1 && rb != null)
        {
            int nextPoint = nowPoint + 1;

            if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            {
                Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                rb.MovePosition(toVector);
            }
            else
            {
                rb.MovePosition(movePoint[nextPoint].transform.position);
                ++nowPoint;

                if (nowPoint + 1 >= movePoint.Length)
                {
                    moveNREnd = true;
                    rb.velocity = Vector2.zero;
                }

            }

            myVelocity = (rb.position - oldPos) / Time.deltaTime;
            oldPos = rb.position;

        }
    }

    //���g�̑��x��Ԃ����\�b�h
    public Vector2 GetVelocity()
    {
        if (isFixedPosition)
        {
            if (moveNREnd)
            {
                return Vector2.zero;
            }

            return myVelocity;
        }
        else
        {
            return Vector2.zero;
        }

    }
}
