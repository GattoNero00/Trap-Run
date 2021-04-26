using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    [SerializeField,Range(1,10)] private float speed = 1f; //点滅する速度
    [SerializeField,Range(3,10)] private float blinkingTime = 5f; //点滅させる時間

    private Text text;
    private float Timer; //経過時間
    private float time; //点滅のタイミングを図る値

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
