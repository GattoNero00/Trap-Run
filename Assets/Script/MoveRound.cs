using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveRound : MonoBehaviour
{
    [SerializeField] private float speed; //�����X�s�[�h
    [SerializeField] private float radius; //�~�^���̔��a
    [SerializeField] private bool isFixedPosition = true; //�L�����̈ʒu���Œ肷�邩

    private Rigidbody2D rb;
    private Vector2 oldPos = Vector2.zero; //�O�̃t���[�����̈ʒu
    private Vector2 myVelocity = Vector2.zero; //���x
    private Vector2 startPos; //�����ʒu
    private Vector2 pos; //���݈ʒu

    private float movex; //���x��x����
    private float movey; //���x��y����

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

    //���g�̑��x��Ԃ����\�b�h
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

