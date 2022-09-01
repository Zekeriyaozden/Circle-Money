using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChibiController : MonoBehaviour
{
    public List<SplineComputer> splines;
    public List<GameObject> chibies;
    public GameObject chibiParent;
    void Start()
    {
        int splIndex = 0;
        for (int i = 0; i < 120; i++)
        {
            GameObject chb = null;
            if (i % 3 == 0)
            {
                chb = Instantiate(chibies[0], chibiParent.transform);
            }else if (i % 3 == 1)
            {
                chb = Instantiate(chibies[1], chibiParent.transform);
            }
            else
            {
                chb = Instantiate(chibies[2], chibiParent.transform);
            }

            if (chb != null)
            {
                chb.transform.position = Vector3.zero;
                int _splIndex = splIndex % splines.Count; 
                SplineFollower sf = chb.GetComponent<SplineFollower>();
                sf.spline = splines[_splIndex];
                splIndex++;
            }
        }
    }

    private IEnumerator chib()
    {
        int splIndex = 0;
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(2f);
            GameObject chb = null;
            if (i % 3 == 0)
            {
                chb = Instantiate(chibies[0], chibiParent.transform);
            }else if (i % 3 == 1)
            {
                chb = Instantiate(chibies[1], chibiParent.transform);
            }
            else
            {
                chb = Instantiate(chibies[2], chibiParent.transform);
            }

            if (chb != null)
            {
                chb.transform.position = Vector3.zero;
                int _splIndex = splIndex % splines.Count; 
                SplineFollower sf = chb.GetComponent<SplineFollower>();
                sf.spline = splines[_splIndex];
                splIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
