using System;
using UnityEditor;
using UnityEngine;
using System.IO;
 
public class ReverseAnimationContext:MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        //anim.ForceStateNormalizedTime(1f);
        //anim.speed = -1f;
        Debug.Log(anim.speed);
    }
}
 