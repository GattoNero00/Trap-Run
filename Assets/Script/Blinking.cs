using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    [SerializeField,Range(1,10)] private float speed = 1f; //�_�ł��鑬�x
    [SerializeField,Range(3,10)] private float blinkingTime = 5f; //�_�ł����鎞��

    private Text text;
    private float Timer; //�o�ߎ���
    private float time; //�_�ł̃^�C�~���O��}��l

    private void Start()
    {
        text = GetComponent<Text>();
        Timer = 0;
    }

    void Update()
    {
        if (Timer < blinkingTime)
        {
            text.color = GetAlphaColor(text.color);
        }

        Timer += Time.deltaTime;
    }

    private Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 3.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
