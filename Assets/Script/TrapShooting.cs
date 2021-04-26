using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet; //��������e��prefab
    [SerializeField] private bool rightDirection = true; //�E�����ɒe�𔭎˂��邩�ǂ���
    [SerializeField] private float bulletSpeed = 7f; //�e�̑���
    [SerializeField, Range(1,5)] private float respawnTime = 2f; //�N�[���^�C��
    [SerializeField] private bool isRandomSpeed = false; //�e�̑����������_���ɂ��邩�ǂ���

    private PlayerStatus ps;
    private Rigidbody2D rb;
    private Vector2 pos; //��C�̈ʒu
    private Vector2 bulletVelocity; //�e�̑��x

    private void Start()
    {
        ps = GameObject.Find("character").GetComponent<PlayerStatus>();
        rb = bullet.GetComponent<Rigidbody2D>();
        pos = transform.position;
        bulletVelocity = new Vector2(bulletSpeed, 0);

        StartCoroutine(SpawnBullet());
    }

    //�e�ې����R���[�`��
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
