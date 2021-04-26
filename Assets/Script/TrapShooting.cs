using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet; //生成する弾のprefab
    [SerializeField] private bool rightDirection = true; //右方向に弾を発射するかどうか
    [SerializeField] private float bulletSpeed = 7f; //弾の速さ
    [SerializeField, Range(1,5)] private float respawnTime = 2f; //クールタイム
    [SerializeField] private bool isRandomSpeed = false; //弾の速さをランダムにするかどうか

    private PlayerStatus ps;
    private Rigidbody2D rb;
    private Vector2 pos; //大砲の位置
    private Vector2 bulletVelocity; //弾の速度

    private void Start()
    {
        ps = GameObject.Find("character").GetComponent<PlayerStatus>();
        rb = bullet.GetComponent<Rigidbody2D>();
        pos = transform.position;
        bulletVelocity = new Vector2(bulletSpeed, 0);

        StartCoroutine(SpawnBullet());
    }

    //弾丸生成コルーチン
    private IEnumerator SpawnBullet()
    {
        while (true)
        {
            GameObject bullets = Instantiate(bullet) as GameObject;

            if (isRandomSpeed)
            {
                bulletVelocity.x = Random.Range(0.9f, 1.5f) * bulletSpeed;
            }

            if (rightDirection)
            {
                bullets.transform.position = pos + new Vector2(1.0f, 0.2f);
                if(bulletVelocity.x < 0)
                {
                    bulletVelocity.x *= -1;
                }
            }
            else
            {
                bullets.transform.position = pos + new Vector2(-1.0f, 0.2f);
                if (bulletVelocity.x > 0)
                {
                    bulletVelocity.x *= -1;
                }
            }

            bullets.GetComponent<Rigidbody2D>().velocity = bulletVelocity;

            yield return new WaitForSeconds(respawnTime);

            if (ps.isDead)
            {
                break;
            }

        }

    }
}
