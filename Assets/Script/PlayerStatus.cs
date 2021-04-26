using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private GameObject[] playerIcons; //残基表示アイコン
    [SerializeField] private int remainNum = 15; //プレイヤーの残基数
    
    public static int remain = 14;

    private PlayerController pc;
    private BoxCollider2D col;
    private string trapTag = "Trap";
    private bool readyDie; 
    public bool isDead { set; get; } = false; //志望判定
    


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
    
    //死亡時処理
    private void Die()
    {
        //死亡時残基が2以上減ることを防止
        if (readyDie == true)
        {
            readyDie = false;
            remain--;

            StartCoroutine("DieMove");
        }

    }

    //プレイヤーの残基表示
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

    //プレイヤーの残基リセット
    public void ResetRemainNum()
    {
        remain = remainNum - 1;
    }

    //死亡時処理コルーチン
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
