using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject pausePanel; //�ꎞ��~���
    private Button pauseButton; //�ꎞ��~�{�^��
    private Button resumeButton; //�ĊJ�{�^��

    private void Start()
    {
        pausePanel = GameObject.Find("pausePanel");
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();

        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(PauseScene);
        resumeButton.onClick.AddListener(ResumeScene);
    }

    private void PauseScene()
    {
        Time.timeScale = 0;
        AudioManager.Instance.StopBGM();
        pausePanel.SetActive(true);
    }

    private void ResumeScene()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlayBGM("BGM1");
        pausePanel.SetActive(false);
    }
}
