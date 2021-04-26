using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private GameObject[] playerIcons; //�c��\���A�C�R��
    [SerializeField] private int remainNum = 15; //�v���C���[�̎c�
    
    public static int remain = 14;

    private PlayerController pc;
    private BoxCollider2D col;
    private string trapTag = "Trap";
    private bool readyDie; 
    public bool isDead { set; get; } = false; //�u�]����
    


    void Awake()
    {
        readyDie = true;
        isDead = false;
        pc = GetComponent<PlayerController>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == trapTag)
        {
            Die();
        }
    }
    
    //���S������
    private void Die()
    {
        //���S���c�2�ȏ㌸�邱�Ƃ�h�~
        if (readyDie == true)
        {
            readyDie = false;
            remain--;

            StartCoroutine("DieMove");
        }

    }

    //�v���C���[�̎c��\��
    public void UpdatePlayerIcons()
    {
        for (int i = 0; i < playerIcons.Length; i++)
        {
            if (remain < i)
            {
                playerIcons[i].SetActive(false);
            }
            else if(remain >= i)
            {
                playerIcons[i].SetActive(true);
            }
        }
    }

    //�v���C���[�̎c��Z�b�g
    public void ResetRemainNum()
    {
        remain = remainNum - 1;
    }

    //���S�������R���[�`��
    private IEnumerator DieMove()
    {
        AudioManager.Instance.PlaySE("Damage");
        col.enabled = false;
        isDead = true;
        pc.dontMove = true;

        yield return new WaitForSeconds(1);

        pc.dontMove = false;
        Destroy(this.gameObject);

        if (remain >= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (remain < 0)
        {
            AudioManager.Instance.StopBGM();
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
