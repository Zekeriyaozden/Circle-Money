using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiUpgradeController : MonoBehaviour
{
    private GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.targetFlag2 = false;
        }
    }
}
