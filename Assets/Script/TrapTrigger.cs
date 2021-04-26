using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] private GameObject trap = null;
    private string playerTag = "Player";

    //�ڐG������^�[�Q�b�g���N������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            trap.SetActive(true);
        }
    }
}
