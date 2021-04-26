using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneChanger : MonoBehaviour
{
    private string playerTag = "Player";

    //次のステージへ移動
    public void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.tag == playerTag)
        {
            AudioManager.Instance.StopSE();
            int n = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++n);
        }
    }

    //スタートボタン処理
    public void OnClickStart()
    {
        AudioManager.Instance.PlaySE("select01");
        AudioManager.Instance.PlayBGM("BGM1");
        SceneManager.LoadScene(1);
    }

    //リトライボタン処理
    public void OnClickRetry()
    {
        AudioManager.Instance.PlaySE("select01");
        SceneManager.LoadScene("GameStartScene");
    }
}
