using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class CustomerBehavController : MonoBehaviour
{
    public double target;
    private SplineFollower sf;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        sf = GetComponent<SplineFollower>();
        if (!(target > 0.08))
        {
            target = 0.1d;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sf.GetPercent() - 0.01d < target && sf.GetPercent() + 0.01d > target)
        {
            sf.follow = false;
            anim.SetBool("Idle",true);
            anim.SetBool("Walk",false);
        }
        else
        {
            anim.SetBool("Walk",true);
            anim.SetBool("Idle",false);
            sf.follow = true;
        }
    }
}
