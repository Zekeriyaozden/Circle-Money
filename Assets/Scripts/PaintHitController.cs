using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintHitController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
