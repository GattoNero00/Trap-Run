using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    [SerializeField] private Text timeText; //クリアタイム

    private void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySE("GameClear");

        timeText.enabled = false;

        StartCoroutine(ExpressTime());

    }

    private IEnumerator ExpressTime()
    {
        yield return new WaitForSeconds(2);

        timeText.text = Timer.Instance.TimeCheck();
        timeText.enabled = true;

        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("GameStartScene");
    }
}
