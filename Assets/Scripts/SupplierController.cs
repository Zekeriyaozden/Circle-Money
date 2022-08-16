using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SupplierController : MonoBehaviour
{
    private SplineFollower sfl;
    public bool canStop;
    void Start()
    {
        sfl = gameObject.GetComponent<SplineFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(sfl.GetPercent());
        if (sfl.GetPercent() > (double) 0.47 && sfl.GetPercent() < (double) 0.53 && canStop)
        {
            sfl.follow = false;
        }
        else
        {
            sfl.follow = true;
        }
    }
}
