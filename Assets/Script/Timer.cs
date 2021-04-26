using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : SingletonMonoBehaviour<Timer>
{
    private float startTime; //始まりの時間
    private float second; //経過時間
    private int minute; //経過時間を分換算する
    private string clearTime; //クリアタイム


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

    //クリアタイムを返すメソッド
    public string TimeCheck()
    {
        clearTime = "ClearTime  " + minute.ToString("00") + ":" + ((int)second % 60).ToString("00");
        return clearTime;
    }
}
