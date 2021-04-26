using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    private string checkerTag = "Checker";
    private bool isCeiling = false; //“ª‚ª“Vˆä‚ÉG‚ê‚Ä‚¢‚é‚©
    private bool isCeilingEnter = false; 
    private bool isCeilingStay = false;
    private bool isCeilingExit = false;

    public bool IsCeiling()
    {
        if (isCeilingEnter || isCeilingStay)
        {
            isCeiling = true;
        }

        if (isCeilingExit)
        {
            isCeiling = false;
        }

        isCeilingEnter = false;
        isCeilingStay = false;
        isCeilingExit = false;

        return isCeiling;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != checkerTag)
        {
            isCeilingEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag != checkerTag)
        {
            isCeilingStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag != checkerTag)
        {
            isCeilingExit = true;
        }
    }
}
