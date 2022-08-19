using System;
using UnityEditor;
using UnityEngine;
using System.IO;
 
public class ReverseAnimationContext:MonoBehaviour
{
    private Animator anim;
    private GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = gameObject.GetComponent<Animator>();
        anim.speed = gm.carAnimSpeed;
        Debug.Log(anim.speed);
    }
}
 