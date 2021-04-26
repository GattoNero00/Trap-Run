using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedMove : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            target.GetComponent<MoveObject>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            target.GetComponent<MoveObject>().enabled = false;
        }
    }
}
