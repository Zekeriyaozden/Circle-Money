using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<int> hiredWorker;
    public List<int> WorkingWorker;
    public SplineComputer firstSpline;
    public GameObject Player;
    public GameObject PlayerReferance;
    public GameObject WorkerPrefabs;
    public GameObject hireCor;
    public float lerpSpeed;
    public float bezierTime;
    [HideInInspector]
    public bool isPlayerStopped;
    public int stackSize;
    public int MaxStackSize;
    //----------------------------------------
    public float workerHiredSpeed;
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
