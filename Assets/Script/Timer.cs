using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : SingletonMonoBehaviour<Timer>
{
    private float startTime; //�n�܂�̎���
    private float second; //�o�ߎ���
    private int minute; //�o�ߎ��Ԃ𕪊��Z����
    private string clearTime; //�N���A�^�C��


    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        startTime = Time.time;
    }

    private void Update()
    {
        second = Time.time - startTime;

        minute = (int)second / 60;
    }

    //�N���A�^�C����Ԃ����\�b�h
    public string TimeCheck()
    {
        clearTime = "ClearTime  " + minute.ToString("00") + ":" + ((int)second % 60).ToString("00");
        return clearTime;
    }
}
