using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaSound : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlaySE("rumbling");
    }
}
