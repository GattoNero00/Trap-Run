using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneChanger : MonoBehaviour
{
    private string playerTag = "Player";

    //���̃X�e�[�W�ֈړ�
    public void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.tag == playerTag)
        {
            AudioManager.Instance.StopSE();
            int n = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++n);
        }
    }

    //�X�^�[�g�{�^������
    public void OnClickStart()
    {
        AudioManager.Instance.PlaySE("select01");
        AudioManager.Instance.PlayBGM("BGM1");
        SceneManager.LoadScene(1);
    }

    //���g���C�{�^������
    public void OnClickRetry()
    {
        AudioManager.Instance.PlaySE("select01");
        SceneManager.LoadScene("GameStartScene");
    }
}
