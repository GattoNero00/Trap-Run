using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObject : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoint; //移動目標
    [SerializeField] private float speed = 0.5f; //Objが動く速さ
    [SerializeField] private bool canReturn = true; //往復するかどうか
    [SerializeField] private bool isFixedPosition = false; //自Objに乗っているObjを固定するかどうか

    private Rigidbody2D rb;
    private int nowPoint = 0; 
    private bool returnPoint = false; //全ての経路を通過したか
    private bool moveNREnd = false; //MoveNotReternが最後まで到達したかどうか
    private Vector2 oldPos = Vector2.zero; //前のフレーム時のポジション
    private Vector2 myVelocity = Vector2.zero; //自分の速度(参照用)

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

    //往復するときの移動処理
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

    //往復しない時の移動処理
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

    //自身の速度を返すメソッド
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
