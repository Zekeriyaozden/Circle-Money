using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerReferance;
    public float lerpSpeed;
    public float bezierTime;
    [HideInInspector]
    public bool isPlayerStopped;
    public int stackSize;
    public int MaxStackSize;
    //----------------------------------------
    public int maxPaintRequire;
    public int maxPieceRequire;
    public GameObject stackItemPiecePref;
    public GameObject stackItemPaintPref;
    public float carAnimSpeed;
    
    
    void Start()
    {
        Application.targetFrameRate = 200;
    }
    
    
    void Update()
    {
        
    }
}
