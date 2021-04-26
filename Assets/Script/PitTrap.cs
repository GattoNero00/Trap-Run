using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitTrap : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool YfirstTime = false; //ˆê‰ñ–Ú‚ðŒ©“¦‚·‚©‚Ç‚¤‚©

    private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            if(!YfirstTime)
            {
                target.SetActive(false);
            }

        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            if (YfirstTime)
            {
                YfirstTime = false;
            }
        }
    }
    

}
