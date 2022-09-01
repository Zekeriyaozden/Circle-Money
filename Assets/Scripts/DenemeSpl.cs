using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using Random = UnityEngine.Random;

public class DenemeSpl : MonoBehaviour
{
    private bool percent;
    private SplineFollower sf;
    public double db;
    void Start()
    {
        percent = true;
        float perc;
        sf = GetComponent<SplineFollower>();
        perc = Random.Range(0f, 1f);
        db = (double)perc;
        StartCoroutine(starts());
    }

    IEnumerator starts()
    {
        float percs = Random.Range(0f, .1f);
        yield return new WaitForSeconds(percs);
        gameObject.GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (percent)
        {
            sf.SetPercent(db);            
            percent = false;
        }
        else
        {
           db = sf.GetPercent();
        }
    }
}
