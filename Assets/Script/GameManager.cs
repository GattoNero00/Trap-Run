using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject character;
    private Text stageNumText; //�X�e�[�W�̔ԍ�
    private PlayerStatus ps;
    private string sceneName; //���݂̃V�[���̖��O
    private string titleScene = "GameStartScene";
    private string firstStage = "Stage1";
    private string endScene = "GameOverScene";
    private string lastStage = "LastStage";

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;


        if (sceneName != titleScene && sceneName != endScene)
        {
            character = GameObject.Find("character");
            stageNumText = GameObject.Find("StageNum").GetComponent<Text>();
            ps = character.GetComponent<PlayerStatus>();

            if (sceneName == firstStage)
            {
                ps.ResetRemainNum(); //�v���C���[�̎c��Z�b�g
            }

            ps.UpdatePlayerIcons(); //�v���C���[�̎c��̕\�������Z�b�g
            stageNumText.text = SceneManager.GetActiveScene().buildIndex.ToString();
        }

        if(sceneName == endScene || sceneName == lastStage)
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.StopSE();
        }
    }
}
