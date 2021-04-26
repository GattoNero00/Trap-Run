using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    private float Timer;
    private float destroyTime; //Á–Å‚·‚é‚Ü‚Å‚ÌŽžŠÔ

    private void Start()
    {
        Timer = 0;
        destroyTime = 4f;
    }

    private void Update()
    {
        if(Timer > destroyTime)
        {
            Destroy(this.gameObject);
        }
        Timer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
