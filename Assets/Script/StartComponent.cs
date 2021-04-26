using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComponent : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private string groundCheckTag = "GroundCheck";
    private const string moveFloorTag = "MoveFloor";
    private const string fallBlockTag = "FallBlock";


    private void Start()
    {
        switch (obj.tag)
        {
            case moveFloorTag:
                obj.GetComponent<MoveObject>().enabled = false;
                break;

            case fallBlockTag:
                obj.GetComponent<FallBlock>().enabled = false;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == groundCheckTag)
        {
            switch (obj.tag)
            {
                case fallBlockTag:
                    obj.GetComponent<FallBlock>().enabled = true;
                    break;

                case moveFloorTag:
                    obj.GetComponent<MoveObject>().enabled = true;
                    break;

            }
        }
    }
}
