using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveRound : MonoBehaviour
{
    [SerializeField] private float speed; //動くスピード
    [SerializeField] private float radius; //円運動の半径
    [SerializeField] private bool isFixedPosition = true; //キャラの位置を固定するか

    private Rigidbody2D rb;
    private Vector2 oldPos = Vector2.zero; //前のフレーム時の位置
    private Vector2 myVelocity = Vector2.zero; //速度
    private Vector2 startPos; //初期位置
    private Vector2 pos; //現在位置

    private float movex; //速度のx成分
    private float movey; //速度のy成分

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        oldPos = rb.position;
    }

    void FixedUpdate()
    {
        pos = transform.position;
        float rad = speed * Mathf.Rad2Deg * Time.time; 

        pos.x = radius * Mathf.Cos(rad);
        pos.y = radius * Mathf.Sin(rad);

        transform.position = pos + startPos;

        myVelocity = (rb.position - oldPos) / Time.deltaTime;
        oldPos = rb.position;

    }

    //自身の速度を返すメソッド
    public Vector2 GetVelocity()
    {
        if (isFixedPosition)
        {
            return myVelocity;
        }
        else
        {
            return Vector2.zero;
        }

    }
}

