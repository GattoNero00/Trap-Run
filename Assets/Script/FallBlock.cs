using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 1f; //�������x

    Rigidbody2D rb;
    private bool isStart = false; //���������̊J�n�t���O
    private string playerTag = "Player";

    private void Start()
    {
        this.tag = "Trap";
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = fallSpeed;
        isStart = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != playerTag)
        {
            if (isStart)
            {
                this.tag = "Ground";
                rb.bodyType = RigidbodyType2D.Static; //�n�ʂɐG�ꂽ��^�C�v���Œ�ɂ���
            }
        }
    }
}
