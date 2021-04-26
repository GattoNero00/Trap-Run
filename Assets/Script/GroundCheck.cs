using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string groundTag = "Ground";
    private string moveFloorTag = "MoveFloor";
    private string moveFloorRTag = "MoveFloorRound";
    private string fallFloorTag = "FallFloor";
    private bool isGround = false; //ê⁄ínîªíËÉtÉâÉO
    private bool isGroundEnter, isGroundStay, isGroundExit;


    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }

        if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == moveFloorTag || collision.tag == fallFloorTag || collision.tag == moveFloorRTag)
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == moveFloorTag || collision.tag == fallFloorTag || collision.tag == moveFloorRTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == moveFloorTag || collision.tag == fallFloorTag || collision.tag == moveFloorRTag)
        {
            isGroundExit = true;
        }
    }
}
